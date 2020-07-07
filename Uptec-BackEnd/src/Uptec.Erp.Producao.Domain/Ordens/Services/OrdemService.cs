using System;
using System.Collections.Generic;
using System.Linq;
using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.Services;
using Uptec.Erp.Producao.Domain.Clientes.Interfaces;
using Uptec.Erp.Producao.Domain.Fornecedores.Interfaces;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Producao.Domain.Ordens.Interfaces;
using Uptec.Erp.Producao.Domain.Ordens.Models;
using Uptec.Erp.Producao.Domain.Ordens.Validations;
using Uptec.Erp.Producao.Domain.Pecas.Interfaces;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Ordens.Services
{
    public class OrdemService : BaseService, IOrdemService
    {
        private readonly IOrdemRepository _ordemRepository;
        private readonly ILoteRepository _loteRepository;
        private readonly ILoteService _loteService;
        private readonly IPecaRepository _pecaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IFornecedorRepository _fornecedorRepository;

        public OrdemService(IBus bus, 
                            IOrdemRepository ordemRepository, 
                            ILoteRepository loteRepository,
                            ILoteService loteService,
                            IPecaRepository pecaRepository,
                            IClienteRepository clienteRepository,
                            IFornecedorRepository fornecedorRepository) : base(bus)
        {
            _ordemRepository = ordemRepository;
            _loteRepository = loteRepository;
            _loteService = loteService;
            _pecaRepository = pecaRepository;
            _clienteRepository = clienteRepository;
            _fornecedorRepository = fornecedorRepository;
        }

        public void Add(Ordem ordem)
        {
            if(!ValidateEntity(ordem)) return;

            if(!ValidateBusinessRules(new OrdemCanAddValidation(ordem, _loteRepository).Validate(ordem))) return;

            ordem.SetOrdemNumero(_ordemRepository.GetOrdemSequence());

            var lote = ReservaEstoque(ordem.OrdemNumero, ordem.OrdemLotes.ToList());
            var peca = _pecaRepository.GetById(lote.PecaId);

            if (lote.ClienteId != null)
            {
                var cliente = _clienteRepository.GetById(lote.ClienteId.Value);
                ordem.SetCliente(cliente.Id);
            }

            if (lote.FornecedorId != null)
            {
                var fornecedor = _fornecedorRepository.GetById(lote.FornecedorId.Value);
                ordem.SetFornecedor(fornecedor.Id);
            }

            ordem.SetPeca(peca.Codigo, peca.Descricao); 

            _ordemRepository.Add(ordem);
        }

        public void Finalizar(Guid ordemId, List<OrdemLote> ordemLotes)
        {
            var ordem = _ordemRepository.GetFullById(ordemId);

            if (ordem == null)
            {
                NotifyError("Ordem inexistente.");
                return;
            }

            foreach (var item in ordem.OrdemLotes)
            {
                var itemMatch = ordemLotes.FirstOrDefault(o => o.Id == item.Id);

                if (item.MotivoExpedicao == OrdemMotivoExpedicao.Total)
                    item.SetQtdeProduzida(item.Qtde);
                else
                    item.SetQtdeProduzida(itemMatch.QtdeProduzida.Value);

                item.SetMotivoExpedicao(itemMatch.MotivoExpedicao);
                item.SetValidade(itemMatch.Validade.Value);
            }

            if (!ValidateBusinessRules(new OrdemCanFinalizarValidation(ordem).Validate(ordem))) return;

            foreach (var item in ordem.OrdemLotes)
                AjustarLoteEstoque(ordem.OrdemNumero, item);

            if (ordem.OrdemLotes.Any(o => o.MotivoExpedicao == OrdemMotivoExpedicao.Parcial))
                AddNewOrdem(ordem);

            ordem.SetStatus(StatusOrdem.Expedicao);
            ordem.SetProducao(ordem.OrdemLotes.Sum(o => o.QtdeProduzida.Value));

            _ordemRepository.Update(ordem);
        }

        public void Update(Ordem ordem)
        {
            if (!ValidateEntity(ordem)) return;

            if (!ValidateBusinessRules(new OrdemCanUpdateValidation(_ordemRepository).Validate(ordem))) return;

            _ordemRepository.Update(ordem);
        }

        public void Delete(Guid id)
        {
            var ordem = _ordemRepository.GetFullById(id);

            if (ordem == null)
            {
                NotifyError("Ordem inexistente.");
                return;
            }

            if(ordem.Status != StatusOrdem.Producao)
            {
                NotifyError("Ordem não pode ser cancelada.");
                return;
            }

            foreach (var ol in ordem.OrdemLotes)
            {
                var loteMovimento = new LoteMovimento(Guid.NewGuid(),
                                                  DateTime.Now,
                                                  ol.LoteId,
                                                  ol.LoteSequencia,
                                                  ol.Qtde,
                                                  0,
                                                  "",
                                                  TipoMovimentoEstoque.Entrada,
                                                  $"Referente cancelamento da Ordem de Produção N. {ordem.OrdemNumero}");

                _loteService.AddMovimentoEntradaProducaoCancelada(loteMovimento);
            }

            ordem.SetStatus(StatusOrdem.Cancelada);
            _ordemRepository.Update(ordem);
        }

        public void SetNotaSaida(string numeroNota, Guid ordemLoteId)
        {
            var ordemLote = _ordemRepository.GetOrdemLoteById(ordemLoteId);
            

            if (ordemLote == null)
            {
                NotifyError("Lote da ordem de produ��o inexistente.");
                return;
            }

            var ordem = _ordemRepository.GetById(ordemLote.OrdemId);
            ordem.UpdateStatus();

            ordemLote.SetNotaFiscalSaida(numeroNota);
            _ordemRepository.UpdateOrdemLote(ordemLote);
            _ordemRepository.UpdateStatus(ordem);
        }

        public void UpdateStatus(Guid ordemId)
        {

        }

        private void AddNewOrdem(Ordem ordem)
        {
            var newOrdemLotes = new List<OrdemLote>();
            decimal qtdeTotal = 0;

            var ordemLotes = ordem.OrdemLotes.Where(o => o.MotivoExpedicao == OrdemMotivoExpedicao.Parcial);

            foreach (var item in ordemLotes)
            {
                var lote = _loteRepository.GetById(item.LoteId);

                var qtdeItem = (item.Qtde - item.QtdeProduzida.Value);
                var newItem = new OrdemLote(Guid.Empty, item.LoteNumero, lote.Sequencia, qtdeItem, item.LoteId);
                newOrdemLotes.Add(newItem);
                qtdeTotal += qtdeItem;
            }

            var newOrdem = new Ordem(Guid.Empty, qtdeTotal, newOrdemLotes);
            Add(newOrdem);
        }
        
        private Lote ReservaEstoque(int ordemNumero, List<OrdemLote> ordemLotes)
        {
            Lote lote = null;

            foreach (var item in ordemLotes)
            {
                var historico = $"Reserva de Estoque para Ordem de Produ��o: {ordemNumero}.";

                lote = _loteRepository.GetById(item.LoteId);

                if (item.Qtde < lote.Quantidade)
                    historico = $"Reserva de Estoque para Ordem de Produ��o: {ordemNumero}. Sequ�ncia: {lote.Sequencia} fechada.";

                var loteMovimento = new LoteMovimento(Guid.NewGuid(), 
                                                      DateTime.Now, 
                                                      item.LoteId,
                                                      lote.Sequencia,
                                                      item.Qtde, 
                                                      0, 
                                                      "", 
                                                      TipoMovimentoEstoque.Saida,
                                                      historico);

                _loteService.AddMovimentoSaida(loteMovimento, true);
            }

            return lote;
        }

        //private void AjustarEstoque(int ordemNumero, List<OrdemLote> ordemLotes)
        //{
        //    foreach (var item in ordemLotes)
        //    {
                
        //        var diferenca = item.Qtde - item.QtdeProduzida;

        //        if(diferenca > 0)
        //        {
        //            var lote = _loteRepository.GetById(item.LoteId);

        //            var loteMovimento = new LoteMovimento(Guid.NewGuid(),
        //                                              DateTime.Now,
        //                                              item.LoteId,
        //                                              item.LoteSequencia,
        //                                              diferenca.Value,
        //                                              0,
        //                                              "",
        //                                              TipoMovimentoEstoque.Entrada,
        //                                              $"Sobra de Produ��o, referente a finaliza��o da Ordem de Produ��o: {ordemNumero}.");

        //            _loteService.AddMovimentoEntrada(loteMovimento);
        //        }
        //    }
        //}

        private void AjustarLoteEstoque(int ordemNumero, OrdemLote ordemLote)
        {
            var diferenca = ordemLote.Qtde - ordemLote.QtdeProduzida;
            var historico = (ordemLote.MotivoExpedicao == OrdemMotivoExpedicao.Sobra) ?
                $"Sobra de Produção, referente a finalização da Ordem de Produção: {ordemNumero}." :
                $"Produção Parcial, referente a finalização da Ordem de Produção: {ordemNumero}.";

            if (diferenca > 0)
            {
                //var lote = _loteRepository.GetById(ordemLote.LoteId);

                var loteMovimento = new LoteMovimento(Guid.NewGuid(),
                                                  DateTime.Now,
                                                  ordemLote.LoteId,
                                                  ordemLote.LoteSequencia,
                                                  diferenca.Value,
                                                  0,
                                                  "",
                                                  TipoMovimentoEstoque.Entrada,
                                                  historico);

                _loteService.AddMovimentoEntradaProducaoParcial(loteMovimento, ordemLote.MotivoExpedicao);
            }
        }

        public void Dispose()
        {
            _ordemRepository?.Dispose();
            _loteRepository?.Dispose();
            _pecaRepository?.Dispose();
            _clienteRepository?.Dispose();
            _fornecedorRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
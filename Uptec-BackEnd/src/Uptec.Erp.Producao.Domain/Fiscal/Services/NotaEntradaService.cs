using System;
using System.Linq;
using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.Services;
using Uptec.Erp.Producao.Domain.Clientes.Interfaces;
using Uptec.Erp.Producao.Domain.Componentes.Interfaces;
using Uptec.Erp.Producao.Domain.Componentes.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Validations;
using Uptec.Erp.Producao.Domain.Fornecedores.Interfaces;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Producao.Domain.Pecas.Interfaces;
using Uptec.Erp.Producao.Domain.Pecas.Models;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Fiscal.Services
{
    public class NotaEntradaService : BaseService, INotaEntradaService
    {
        private readonly INotaEntradaRepository _notaEntradaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IPecaRepository _pecaRepository;
        private readonly IComponenteRepository _componenteRepository;
        private readonly IComponenteService _componenteService;
        private readonly ILoteService _loteService;
        private readonly ILoteRepository _loteRepository;

        public NotaEntradaService(IBus bus, 
                                  INotaEntradaRepository fiscalRepository,
                                  IClienteRepository clienteRepository,
                                  IFornecedorRepository fornecedorRepository,
                                  IPecaRepository pecaRepository,
                                  ILoteService loteService,
                                  ILoteRepository loteRepository,
                                  IComponenteRepository componenteRepository,
                                  IComponenteService componenteService) : base(bus)
        {
            _notaEntradaRepository = fiscalRepository;
            _clienteRepository = clienteRepository;
            _fornecedorRepository = fornecedorRepository;
            _pecaRepository = pecaRepository;
            _componenteRepository = componenteRepository;
            _componenteService = componenteService;
            _loteService = loteService;
            _loteRepository = loteRepository;
        }

        public void Add(NotaEntrada notaEntrada)
        {
            if(!ValidateEntity(notaEntrada)) return;

            if(!ValidateBusinessRules(new NotaEntradaCanAddValidation(_notaEntradaRepository).Validate(notaEntrada))) return;

            if (notaEntrada.TipoEmissor == TipoEmissor.Indefinido)
                ProcurarTipoEmissor(notaEntrada);

            _notaEntradaRepository.Add(notaEntrada);
        }

        public void Update(NotaEntrada notaEntrada)
        {
            if (!ValidateEntity(notaEntrada)) return;

            if (!ValidateBusinessRules(new NotaEntradaCanUpdateValidation(_notaEntradaRepository).Validate(notaEntrada))) return;

            _notaEntradaRepository.Update(notaEntrada);
        }

        public void Delete(Guid id)
        {
            var nota = _notaEntradaRepository.GetByIdWithAggregate(id);

            if (!ValidateBusinessRules(new NotaEntradaCanDeleteValidation().Validate(nota))) return;

            nota.Itens.ToList().ForEach(i => i.Delete());
            nota.Delete();

            _notaEntradaRepository.Delete(nota);
        }

        public void DefinirTipoEmissor(Guid id, TipoEmissor tipo)
        {
            var nota = _notaEntradaRepository.GetByIdWithAggregate(id);

            if (!ValidateBusinessRules(new NotaEntradaCanUpdateTipoEmissorValidation().Validate(nota))) return;

            nota.SetTipoEmissor(tipo);

            _notaEntradaRepository.Update(nota);
        }

        public void Conciliar(NotaEntrada notaEntrada)
        {
            if (!ValidateEntity(notaEntrada)) return;

            if (!ValidateBusinessRules(new NotaEntradaCanConciliarValidation(notaEntrada,
                _clienteRepository, _fornecedorRepository, _componenteRepository, _pecaRepository).Validate(notaEntrada))) return;

            if (notaEntrada.TipoEstoque == TipoEstoque.Peca)
            {
                ConciliarPeca(notaEntrada);
                return;
            }

            ConciliarMateriaPrima(notaEntrada);
        }

        public void Cobrir(Guid notaFornecedorId, Guid notaClienteId)
        {
            var notaFornecedor = _notaEntradaRepository.GetByIdWithAggregate(notaFornecedorId);
            var notaCliente = _notaEntradaRepository.GetByIdWithAggregate(notaClienteId);
            var cliente = _clienteRepository.GetByCnpj(notaCliente.CnpjEmissor.Numero);
            var fornecedor = _fornecedorRepository.GetByCnpj(notaFornecedor.CnpjEmissor.Numero);

            if (notaFornecedor == null || notaCliente == null)
            {
                NotifyError("Nota Fiscal inexistente.");
                return;
            }

            foreach (var i in notaFornecedor.Itens)
                i.SetCodigoCliente(_pecaRepository.GetByCodigoFornecedor(fornecedor.Id, i.Codigo).Codigo);

            var lotes = _loteRepository.GetByNumeroNota(notaFornecedor.NumeroNota).ToList();

            if (!ValidateBusinessRules(new NotaEntradaCanCobrirValidation(notaCliente, lotes).Validate(notaFornecedor))) return;


            foreach (var item in notaFornecedor.Itens)
            {
                var equivalente = notaCliente.Itens.ToList()
                    .Find(i => i.Codigo == item.CodigoCliente && i.Unidade == item.Unidade && i.Quantidade == item.Quantidade);

                if(equivalente != null)
                {
                    item.SetStatus(StatusNfEntradaItem.Recebida);
                    item.SetNotaCobertura(notaCliente.NumeroNota);

                    var lote = lotes.FirstOrDefault(l => l.LoteNumero == item.Lote);
                    lote.SetCliente(cliente.Id);
                    lote.SetPrecoEntrada(equivalente.PrecoUnitario);
                    lote.SetCfopEntrada(equivalente.Cfop);
                    lote.SetNotaFiscalCobertura(notaCliente.NumeroNota);
                    
                    _loteService.Update(lote);
                }
            }

            foreach (var item in notaCliente.Itens)
            {
                var equilvalente = notaFornecedor.Itens.ToList()
                    .Find(i => i.CodigoCliente == item.Codigo && i.Unidade == item.Unidade && i.Quantidade == item.Quantidade);

                if(equilvalente != null)
                {
                    item.SetDataFabricacao(equilvalente.DataFabricacao.Value);
                    item.SetDataValidade(equilvalente.DataValidade.Value);
                    item.SetLote(equilvalente.Lote.Value);
                    item.SetStatus(StatusNfEntradaItem.Recebida);
                    item.SetNotaCobertura(notaFornecedor.NumeroNota);
                }
            }

            notaFornecedor.SetStatus();
            notaCliente.SetDataConciliacao(DateTime.Now);
            notaCliente.SetStatus();

            _notaEntradaRepository.Update(notaCliente);
            _notaEntradaRepository.Update(notaFornecedor);
        }

        public NotaEntrada GetConsistida(Guid id)
        {
            var nota = _notaEntradaRepository.GetByIdWithAggregate(id);

            if (nota == null)
            {
                NotifyError("Nota Fiscal inexistente.");
                return nota;
            }

            if (!ValidateEntity(nota))
            {
                SetDomainErrorsInNotaEntrada(nota);
                return nota;
            }

            if (!ValidateBusinessRules(new NotaEntradaCanConciliarValidation(nota, 
                _clienteRepository, _fornecedorRepository, _componenteRepository, _pecaRepository).Validate(nota)))
            {
                SetDomainErrorsInNotaEntrada(nota);
            }

            return nota;
        }

        private void ConciliarPeca(NotaEntrada nota)
        {
            Fornecedor fornecedor = null;
            if (nota.TipoEmissor == TipoEmissor.Fornecedor)
                fornecedor = _fornecedorRepository.GetByCnpj(nota.CnpjEmissor.Numero);

            //Gerando lote
            foreach (var item in nota.Itens)
            {
                Peca peca;
                
                if (nota.TipoEmissor == TipoEmissor.Fornecedor)
                    peca = _pecaRepository.GetByCodigoFornecedor(fornecedor.Id, item.Codigo);
                else
                   peca = _pecaRepository.GetByCodigo(item.Codigo);
                
                var lote = new Lote(Guid.NewGuid(), nota.DataConciliacao.Value, peca.Id, peca.Tipo, item.Quantidade, 
                    item.PrecoUnitario, item.Cfop, nota.NumeroNota, item.DataFabricacao.Value, item.DataValidade.Value,
                    item.Localizacao, item.QtdeConcilia.Value);

                item.SetStatus(StatusNfEntradaItem.Recebida);

                if (nota.TipoEmissor == TipoEmissor.Cliente)
                {
                    var cliente = _clienteRepository.GetByCnpj(nota.CnpjEmissor.Numero);
                    lote.SetCliente(cliente.Id);
                }
                else
                {
                    lote.SetFornecedor(fornecedor.Id);
                    if (peca.Tipo == TipoPeca.Peca)
                    {
                        lote.SetEhCobertura();
                        item.SetStatus(StatusNfEntradaItem.ACobrir);
                    }    
                }

                item.SetLote(_loteService.Add(lote));
            }

            //Setando status da nota conforme itens
            nota.SetStatus();

            _notaEntradaRepository.Update(nota);
        }

        private void ConciliarMateriaPrima(NotaEntrada nota)
        {
            //gerarando movimento de entrada na mat�ria prima
            foreach (var item in nota.Itens)
            {
                var componente = _componenteRepository.GetByCodigo(item.Codigo);
                var componenteMovimento = new ComponenteMovimento(Guid.NewGuid(), componente.Id, item.Quantidade,
                    TipoMovimentoEstoque.Entrada, item.PrecoUnitario, nota.NumeroNota,
                    $"Entrada por conciliacao referente Nfe:{nota.NumeroNota}");
                _componenteService.AddMovimentoEntrada(componenteMovimento);
                item.SetStatus(StatusNfEntradaItem.Recebida);
            }

            nota.SetStatus();
            _notaEntradaRepository.Update(nota);
        }

        private void SetDomainErrorsInNotaEntrada(NotaEntrada nota)
        {
            foreach (var erro in nota.Validation.Result.Errors)
            {
                nota.Inconsistencias.Add(erro.ErrorMessage);
            }

            foreach (var erro in nota.Validation.SystemResult.Errors)
            {
                nota.Inconsistencias.Add(erro.ErrorMessage);
            }
        }

        private void ProcurarTipoEmissor(NotaEntrada nota)
        {
            var cliente = _clienteRepository.GetByCnpj(nota.CnpjEmissor.Numero);
            var fornecedor = _fornecedorRepository.GetByCnpj(nota.CnpjEmissor.Numero);

            if (cliente != null && fornecedor != null)
                return;

            if(cliente != null)
            {
                nota.SetTipoEmissor(TipoEmissor.Cliente);
                return;
            }

            if (fornecedor != null)
            {
                nota.SetTipoEmissor(TipoEmissor.Fornecedor);
                return;
            }
        }

        public void Dispose()
        {
            _notaEntradaRepository?.Dispose();
            _loteRepository?.Dispose();
            _pecaRepository?.Dispose();
            _componenteRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

        
    }
}
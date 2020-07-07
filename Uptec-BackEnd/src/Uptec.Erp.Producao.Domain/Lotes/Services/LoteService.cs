using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.Services;
using System;
using Uptec.Erp.Producao.Domain.Componentes.Interfaces;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Producao.Domain.Lotes.Validations;
using Uptec.Erp.Producao.Domain.Pecas.Interfaces;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Cfop;
using ComponenteMovimentoAlias = Uptec.Erp.Producao.Domain.Componentes.Models.ComponenteMovimento;

namespace Uptec.Erp.Producao.Domain.Lotes.Services
{
    public class LoteService : BaseService, ILoteService
    {
        private readonly ILoteRepository _loteRepository;
        private readonly IPecaRepository _pecaRepository;
        private readonly IComponenteService _componenteService;

        public LoteService(IBus bus, ILoteRepository loteRepository, IPecaRepository pecaRepository, IComponenteService componenteService) : base(bus)
        {
            _loteRepository = loteRepository;
            _pecaRepository = pecaRepository;
            _componenteService = componenteService;
        }

        public int Add(Lote lote)
        {
            if (!ValidateEntity(lote)) return 0;

            if (!ValidateBusinessRules(new LoteCanAddValidation(_loteRepository).Validate(lote))) return 0;

            var movimento = new LoteMovimento(Guid.NewGuid(), lote.Data, lote.Id, 0, lote.Quantidade, lote.PrecoEntrada,
                lote.NotaFiscal, TipoMovimentoEstoque.Entrada,
                $"Geracao do lote via conciliacao de Nfe. {lote.NotaFiscal} na data de " +
                $"{lote.Data.ToString("dd/MM/yyyy")} no valor de {lote.PrecoEntrada} a unidade");


            var loteNumero = _loteRepository.GetLoteSequence();

            lote.SetLoteNumero(loteNumero);

            _loteRepository.Add(lote);
            _loteRepository.AddMovimento(movimento);

            return loteNumero;
        }

        public void Update(Lote lote)
        {
            if (!ValidateEntity(lote)) return;

            if (!ValidateBusinessRules(new LoteCanUpdateValidation(_loteRepository).Validate(lote))) return;

            _loteRepository.Update(lote);
        }

        public void UpdateSequencia(Lote lote)
        {
            if (!ValidateEntity(lote)) return;

            if (!ValidateBusinessRules(new LoteCanUpdateValidation(_loteRepository).Validate(lote))) return;

            lote.AddSequencia();
            _loteRepository.UpdateSequenciaLote(lote);
        }

        public void Delete(Guid id)
        {
            var Lote = _loteRepository.GetById(id);

            if (Lote == null)
            {
                NotifyError("Lote inexistente.");
                return;
            }

            Lote.Delete();
            _loteRepository.Delete(Lote);
        }

        public LoteDadosSaida GetLoteDadosSaida(Guid id)
        {
            var loteDadosSaida = new LoteDadosSaida();

            var lote = _loteRepository.GetById(id);

            if (lote == null)
            {
                NotifyError("Lote inexistente.");
                return loteDadosSaida;
            }

            //var ehCobertura = !lote.NotaFiscalCobertura.IsNullOrWhiteSpace();

            var peca = _pecaRepository.GetById(lote.PecaId);

            if (peca == null)
            {
                NotifyError("Peça inexistente.");
                return loteDadosSaida;
            }

            loteDadosSaida.Id = lote.Id;
            loteDadosSaida.LoteNumero = lote.LoteNumero;
            loteDadosSaida.LoteSequencia = lote.Sequencia;
            loteDadosSaida.PrecoSaidaRemessa = lote.PrecoEntrada;
            loteDadosSaida.PrecoSaidaServico = peca.PrecoSaida;
            loteDadosSaida.CfopSaidaRemessa = CfopUptec.GetCfopSaidaRemessa(lote.CfopEntrada, lote.EhCobertura);
            loteDadosSaida.CfopSaidaServico = CfopUptec.GetCfopSaidaServico(lote.CfopEntrada, lote.EhCobertura);
            loteDadosSaida.CodigoPeca = peca.Codigo;
            loteDadosSaida.CodigoPecaSaida = peca.CodigoSaida;
            loteDadosSaida.DescricaoPeca = peca.Descricao;
            loteDadosSaida.Ncm = peca.Ncm;
            loteDadosSaida.UnidadeMedida = peca.Unidade;

            return loteDadosSaida;
        }

        public void Dispose()
        {
            _loteRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

        #region Movimenta��o

        public void AddMovimentoEntrada(LoteMovimento movimento)
        {
            if (!ValidateEntity(movimento)) return;

            var lote = _loteRepository.GetById(movimento.LoteId);

            movimento.SetLoteSequencia(lote.Sequencia);

            _loteRepository.AddMovimento(movimento);

            lote.AddQuantidade(movimento.Quantidade);

            _loteRepository.Update(lote);
        }

        public void AddMovimentoEntradaProducaoParcial(LoteMovimento movimento, OrdemMotivoExpedicao motivoEntrada)
        {
            if (!ValidateEntity(movimento)) return;

            var lote = _loteRepository.GetById(movimento.LoteId);

            _loteRepository.AddMovimento(movimento);

            if (lote.Status == LoteStatus.Fechado && motivoEntrada == OrdemMotivoExpedicao.Parcial)
            {
                lote.AddSequencia();
                _loteRepository.UpdateSequenciaLote(lote);
            }

            lote.AddQuantidade(movimento.Quantidade);

            _loteRepository.UpdateLoteSaldo(lote);

        }

        public void AddMovimentoEntradaProducaoCancelada(LoteMovimento movimento)
        {
            if (!ValidateEntity(movimento)) return;

            var lote = _loteRepository.GetById(movimento.LoteId);

            _loteRepository.AddMovimento(movimento);

            if (lote.Status == LoteStatus.Fechado)
            {
                lote.AddSequencia();
                _loteRepository.UpdateSequenciaLote(lote);
            }

            lote.AddQuantidade(movimento.Quantidade);

            _loteRepository.UpdateLoteSaldo(lote);
        }

        public void AddMovimentoSaida(LoteMovimento movimento, bool controlaSequencia = false)
        {
            if (!ValidateEntity(movimento)) return;

            var lote = _loteRepository.GetByIdWithAggregate(movimento.LoteId);

            if (!ValidateBusinessRules(new LoteMovimentoCanAddSaidaValidation(lote).Validate(movimento))) return;

            movimento.SetLoteSequencia(lote.Sequencia);
            _loteRepository.AddMovimento(movimento);

            if (controlaSequencia && movimento.Quantidade < lote.Saldo)
                lote.AddSequencia();

            lote.SubQuantidade(movimento.Quantidade);

            _loteRepository.Update(lote);

            foreach (var componente in lote.Peca.Componentes)
            {
                _componenteService.AddMovimentoSaida(new ComponenteMovimentoAlias(Guid.NewGuid(), componente.ComponenteId,
                                                     (componente.Quantidade * movimento.Quantidade),
                                                     TipoMovimentoEstoque.Saida,
                                                     0.1m,
                                                     movimento.NotaFiscal,
                                                     movimento.Historico
                                                    ));
            }
        }
        #endregion
    }
}
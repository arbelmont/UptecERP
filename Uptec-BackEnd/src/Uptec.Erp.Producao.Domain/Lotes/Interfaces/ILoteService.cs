using System;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Lotes.Interfaces
{
    public interface ILoteService : IDisposable
    {
        int Add(Lote lote);
        void Update(Lote lote);
        void UpdateSequencia(Lote lote);
        void Delete(Guid id);
        LoteDadosSaida GetLoteDadosSaida(Guid id);
        void AddMovimentoEntrada(LoteMovimento movimento);
        void AddMovimentoEntradaProducaoParcial(LoteMovimento movimento, OrdemMotivoExpedicao motivoEntrada);
        void AddMovimentoEntradaProducaoCancelada(LoteMovimento movimento);
        void AddMovimentoSaida(LoteMovimento movimento, bool controlaSequencia = false);
        //void UpdateMovimento(LoteMovimento movimento);
    }
}
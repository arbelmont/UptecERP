using System;
using System.Collections.Generic;
using Definitiva.Shared.Domain.Models;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Producao.Domain.Ordens.Models;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Interfaces;

namespace Uptec.Erp.Producao.Domain.Lotes.Interfaces
{
    public interface ILoteRepository : IRepository<Lote>
    {
        Lote GetByIdWithAggregate(Guid id);
        Lote GetByNumero(int numeroLote);
        ICollection<Lote> GetByNumeroNota(string numeroNota);
        ICollection<Lote> GetByNumeroNotaCobertura(string numeroNota);
        ICollection<Lote> GetByNumeroNotaOuCobertura(string numeroNota);
        ICollection<Lote> GetByNumeroNotaOuCoberturaWithAggregate(string numeroNota);
        Paged<Lote> GetPaged(int pageNumber, int pageSize, bool showLoteFechado, string search);
        Paged<Lote> GetPaged(int pageNumber, int pageSize, Guid pecaId, bool showLoteFechado);
        Paged<Lote> GetFullPagedByPeca(int pageNumber, int pageSize, Guid pecaId, bool showLoteFechado);
        LoteMovimento AddMovimento(LoteMovimento movimento);
        LoteMovimento GetMovimentoById(Guid movimentoId);
        Paged<LoteMovimento> GetPagedMovimento(Guid loteId, DateTime startDate, DateTime endDate, int pageNumber, int pageSize);
        Paged<LoteMovimento> GetPagedMovimento(Guid loteId, int pageNumber, int pageSize);
        IEnumerable<Lote> GetLotesEmbalagemToNfeSaida(Guid destinatarioId, TipoDestinatario tipoDestinatario);
        IEnumerable<Lote> GetLotesPecaToNfeSaida(Guid destinatarioId, TipoDestinatario tipoDestinatario);
        void UpdateSequenciaLote(Lote lote);
        void UpdateLoteSaldo(Lote lote);
        int GetLoteSequence();
        int GetLoteSequenceLastUsed();
        IEnumerable<LoteSaldo> GetLotesSaldo();
    }
}
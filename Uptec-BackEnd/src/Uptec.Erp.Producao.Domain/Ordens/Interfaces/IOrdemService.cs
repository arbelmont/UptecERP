using System;
using System.Collections.Generic;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Producao.Domain.Ordens.Models;

namespace Uptec.Erp.Producao.Domain.Ordens.Interfaces
{
    public interface IOrdemService : IDisposable
    {
        void Add(Ordem ordem);
        void Finalizar(Guid ordemId, List<OrdemLote> OrdemLotes);
        void Update(Ordem ordem);
        void Delete(Guid id);
        void SetNotaSaida(string numeroNota, Guid ordemLoteId);
    }
}
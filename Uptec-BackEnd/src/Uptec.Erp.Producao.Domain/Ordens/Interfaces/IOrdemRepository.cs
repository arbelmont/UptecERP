using Definitiva.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using Uptec.Erp.Producao.Domain.Ordens.Models;
using Uptec.Erp.Shared.Domain.Interfaces;

namespace Uptec.Erp.Producao.Domain.Ordens.Interfaces
{
    public interface IOrdemRepository : IRepository<Ordem>
    {
        Paged<Ordem> GetPaged(int pageNumber, int pageSize);
        Paged<Ordem> GetPaged(int pageNumber, int pageSize, int status, string campo, string search);
        IEnumerable<OrdemLote> GetOrdemLotesToNfeSaida(Guid clienteId);
        Ordem GetFullById(Guid id);
        Ordem GetSuperFullById(Guid id);
        OrdemLote GetOrdemLoteById(Guid ordemLoteId);
        OrdemLote GetOrdemLoteFullById(Guid ordemLoteId);
        OrdemLote UpdateOrdemLote(OrdemLote ordemLote);
        void UpdateStatus(Ordem ordem);
        int GetOrdemSequence();
        IEnumerable<LinhaProducao> GetLinhaProducao();
    }
}
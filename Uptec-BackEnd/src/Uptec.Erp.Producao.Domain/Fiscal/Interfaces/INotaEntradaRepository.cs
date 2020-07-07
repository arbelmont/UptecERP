using Definitiva.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Shared.Domain.Interfaces;

namespace Uptec.Erp.Producao.Domain.Fiscal.Interfaces
{
    public interface INotaEntradaRepository : IRepository<NotaEntrada>
    {
        NotaEntrada GetByNumeroNota(string numeroNota);
        NotaEntrada GetByIdWithAggregate(Guid id);
        Lote GetLoteByNumeroNota(string numeroNota);
        Paged<NotaEntrada> GetPaged(int pageNumber, int pageSize, int tipoEmissor, int status, DateTime startDate, DateTime endDate, string search );
        int GetQtdeNotasAcobrir();
        List<NotaEntrada> GetNotasClientesConciliar();
        NotaEntradaItens UpdateItem(NotaEntradaItens item);
    }
}
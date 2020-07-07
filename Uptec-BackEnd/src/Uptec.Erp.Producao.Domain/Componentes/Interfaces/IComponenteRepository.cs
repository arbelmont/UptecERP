using System;
using Definitiva.Shared.Domain.Models;
using Uptec.Erp.Producao.Domain.Componentes.Models;
using Uptec.Erp.Shared.Domain.Interfaces;

namespace Uptec.Erp.Producao.Domain.Componentes.Interfaces
{
    public interface IComponenteRepository : IRepository<Componente>
    {
        void UpdateSaldo(Componente componente);
        Componente GetByIdWithAggregate(Guid id);
        Componente GetByCodigo(string codigo);
        Paged<Componente> GetPaged(int pageNumber, int pageSize, string search);
        ComponenteMovimento GetMovimentoById(Guid movimentoId);
        ComponenteMovimento AddMovimento(ComponenteMovimento movimento);
        ComponenteMovimento UpdateMovimento(ComponenteMovimento movimento);
        Paged<ComponenteMovimento> GetPagedMovimento(Guid componenteId, DateTime startDate, DateTime endDate, int pageNumber, int pageSize);
    }
}
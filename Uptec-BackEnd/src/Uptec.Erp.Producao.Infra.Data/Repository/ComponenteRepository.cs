using Definitiva.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Linq;
using Uptec.Erp.Producao.Domain.Componentes.Interfaces;
using Uptec.Erp.Producao.Domain.Componentes.Models;
using Uptec.Erp.Producao.Infra.Data.Context;

namespace Uptec.Erp.Producao.Infra.Data.Repository
{
    public class ComponenteRepository : Repository<Componente>, IComponenteRepository
    {
        public ComponenteRepository(ProducaoContext context) : base(context) { }

        public void UpdateSaldo(Componente componente)
        {
            //Dbset.Update(componente); // TODO Verificar porque não está funcionando

            var sql = "UPDATE Componentes SET Quantidade = @saldo WHERE Id = @id";
            var saldo = new SqlParameter("@saldo", componente.Quantidade);
            var id = new SqlParameter("@id", componente.Id.ToString());

            Db.Database.ExecuteSqlCommand(sql, parameters: new[] { saldo, id });
        }

        public Componente GetByIdWithAggregate(Guid id)
        {
            return Dbset.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public Componente GetByCodigo(string codigo)
        {
            return Dbset.AsNoTracking().FirstOrDefault(p => p.Codigo == codigo);
        }

        public Paged<Componente> GetPaged(int pageNumber, int pageSize, string search)
        {
            var skip = (pageNumber - 1) * pageSize;

            var paged = new Paged<Componente>
            {
                List = Dbset.AsNoTracking()
                    .Where(p => p.Codigo.Contains(search) || p.Descricao.Contains(search))
                    .OrderBy(p => p.Descricao).Skip(skip).Take(pageSize),
                Total = Dbset.AsNoTracking()
                    .Count(p => p.Codigo.Contains(search) || p.Descricao.Contains(search))
            };

            return paged;
        }

        public ComponenteMovimento GetMovimentoById(Guid movimentoId)
        {
            return Db.ComponenteMovimentos.AsNoTracking().FirstOrDefault(cm => cm.Id == movimentoId);
        }

        public ComponenteMovimento AddMovimento(ComponenteMovimento movimento)
        {
            return Db.ComponenteMovimentos.Add(movimento).Entity;
        }

        public ComponenteMovimento UpdateMovimento(ComponenteMovimento movimento)
        {
            return Db.ComponenteMovimentos.Update(movimento).Entity;
        }

        public Paged<ComponenteMovimento> GetPagedMovimento(Guid componenteId, DateTime startDate, DateTime endDate, int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;

            var paged = new Paged<ComponenteMovimento>
            {
                List = Db.ComponenteMovimentos.AsNoTracking()
                    .Where(c => c.Data >= startDate && c.Data.Date <= endDate && c.ComponenteId == componenteId)
                    .OrderByDescending(c => c.Data).Skip(skip).Take(pageSize),
                Total = Db.ComponenteMovimentos.AsNoTracking()
                    .Count(c => c.Data >= startDate && c.Data.Date <= endDate && c.ComponenteId == componenteId)
            };

            return paged;
        }

        
    }
}

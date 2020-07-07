using Definitiva.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Uptec.Erp.Producao.Domain.Pecas.Interfaces;
using Uptec.Erp.Producao.Domain.Pecas.Models;
using Uptec.Erp.Producao.Infra.Data.Context;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Infra.Data.Repository
{
    public class PecaRepository : Repository<Peca>, IPecaRepository
    {
        public PecaRepository(ProducaoContext context)
            : base(context)
        {
        }

        public Peca GetByIdWithAggregate(Guid id)
        {
            return Dbset.AsNoTracking().Include(p => p.Componentes).AsNoTracking().Include(p => p.CodigosFornecedor).AsNoTracking()
                .FirstOrDefault(p => p.Id == id);
        }

        public Peca GetByCodigo(string codigo)
        {
            return Dbset.AsNoTracking().FirstOrDefault(p => p.Codigo == codigo);
        }
        public Peca GetByCodigoFornecedor(Guid fornecedorId, string codigo)
        {
            return Dbset.AsNoTracking().Include(p => p.CodigosFornecedor).AsNoTracking()
                .FirstOrDefault(f => f.CodigosFornecedor.Any(cf => cf.FornecedorId == fornecedorId && cf.FornecedorCodigo == codigo));
        }

        public Paged<Peca> GetPaged(int pageNumber, int pageSize, string search)
        {
            var skip = (pageNumber - 1) * pageSize;

            var paged = new Paged<Peca>
            {
                List = Dbset.AsNoTracking()
                    .Where(p => p.Codigo.Contains(search) || p.Descricao.Contains(search))
                    .OrderBy(p => p.Descricao).Skip(skip).Take(pageSize),
                Total = Dbset.AsNoTracking()
                    .Count(p => p.Codigo.Contains(search) || p.Descricao.Contains(search))
            };

            return paged;
        }

        public IEnumerable<Peca> GetToProducao(string search)
        {
            return Dbset.AsNoTracking().Include(p => p.Cliente)
                .Where(p => p.Codigo.Contains(search) || p.Descricao.Contains(search) || p.Cliente.NomeFantasia.Contains(search))
                .Where(p => p.Tipo == TipoPeca.Peca)
                .OrderBy(p => p.Descricao);
        }

        public IEnumerable<Peca> GetAllTipoPeca()
        {
            return Dbset.Where(p => p.Tipo == TipoPeca.Peca);
        }

        public void RemoverComponentesNaoInformados(Peca peca)
        {
            var removerComponentes = Db.PecaComponente.AsNoTracking()
                .Where(p => p.PecaId == peca.Id)
                .Where(c => !peca.Componentes.Any(pc => pc.ComponenteId == c.ComponenteId));

            foreach (var componente in removerComponentes)
            {
                Db.PecaComponente.Remove(componente);
            }
        }

        public void RemoverCodigosFornecedoresNaoInformados(Peca peca)
        {
            var removerCodigosFornecedor = Db.PecaFornecedorCodigo.AsNoTracking()
                .Where(p => p.PecaId == peca.Id)
                .Where(c => !peca.CodigosFornecedor.Any(pc => pc.FornecedorId == c.FornecedorId));

            foreach (var fornecedor in removerCodigosFornecedor)
            {
                Db.PecaFornecedorCodigo.Remove(fornecedor);
            }
        }
    }
}

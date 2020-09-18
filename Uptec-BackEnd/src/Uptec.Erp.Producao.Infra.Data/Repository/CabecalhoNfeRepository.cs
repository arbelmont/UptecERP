using Definitiva.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Integracao.Receita;
using Uptec.Erp.Producao.Infra.Data.Context;

namespace Uptec.Erp.Producao.Infra.Data.Repository
{
    public class CabecalhoNfeRepository : Repository<CabecalhoNfe>, ICabecalhoNfeRepository
    {
        public CabecalhoNfeRepository(ProducaoContext context)
            : base(context)
        {
        }

        public Paged<CabecalhoNfe> GetUnmanifested(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;

            var paged = new Paged<CabecalhoNfe>
            {
                List = Dbset.AsNoTracking().Where(c => c.DataManifestacao == null && c.ManifestacaoDestinatario == null),
                Total = Dbset.AsNoTracking().Count(c => c.DataManifestacao == null && c.ManifestacaoDestinatario == null)
            };

            return paged;
        }

        public CabecalhoNfe ObterPorChave(string chave)
        {
            return Dbset.AsNoTracking().FirstOrDefault(c => c.ChaveNfe == chave);
        }

        public int ObterVersaoMax()
        {
            var cabecalho = Db.CabecalhosNfe.FromSql("SELECT TOP 1 * FROM CabecalhosNfe ORDER BY Versao DESC");

            if (cabecalho != null && cabecalho.Any())
                return cabecalho.FirstOrDefault().Versao;
            else
                return 98948;
        }
    }
}

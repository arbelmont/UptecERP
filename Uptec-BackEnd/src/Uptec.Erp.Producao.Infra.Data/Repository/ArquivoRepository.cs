using Uptec.Erp.Producao.Domain.Arquivos.Interfaces;
using Uptec.Erp.Producao.Domain.Arquivos.Models;
using Uptec.Erp.Producao.Infra.Data.Context;

namespace Uptec.Erp.Producao.Infra.Data.Repository
{
    public class ArquivoRepository : Repository<Arquivo>, IArquivoRepository
    {
        public ArquivoRepository(ProducaoContext context)
            : base(context)
        {
        }
    }
}

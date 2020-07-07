using Definitiva.Shared.Domain.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Integracao.Receita;
using Uptec.Erp.Shared.Domain.Interfaces;

namespace Uptec.Erp.Producao.Domain.Fiscal.Interfaces
{
    public interface ICabecalhoNfeRepository : IRepository<CabecalhoNfe>
    {
        Paged<CabecalhoNfe> GetUnmanifested(int pageNumber, int pageSize);
        CabecalhoNfe ObterPorChave(string chave);
        int ObterVersaoMax();
    }
}

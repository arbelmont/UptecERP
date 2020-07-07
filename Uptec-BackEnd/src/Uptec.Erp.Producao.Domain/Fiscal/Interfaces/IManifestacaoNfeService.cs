using Definitiva.Shared.Domain.Models;
using System;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces.Integracao;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Integracao.Receita;

namespace Uptec.Erp.Producao.Domain.Fiscal.Interfaces
{
    public interface IManifestacaoNfeService : IDisposable, INotaFiscalIntegracaoManifestacao
    {
        void Add(CabecalhoNfe cabecalhoNfe);

        void Update(CabecalhoNfe cabecalhoNfe);

        void Delete(CabecalhoNfe cabecalhoNfe);

        Paged<CabecalhoNfe> GetUnmanifested(int pageNumber, int pageSize);
    }
}

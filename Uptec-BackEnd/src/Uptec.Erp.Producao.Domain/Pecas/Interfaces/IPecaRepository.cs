using Definitiva.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using Uptec.Erp.Producao.Domain.Pecas.Models;
using Uptec.Erp.Shared.Domain.Interfaces;

namespace Uptec.Erp.Producao.Domain.Pecas.Interfaces
{
    public interface IPecaRepository : IRepository<Peca>
    {
        Peca GetByIdWithAggregate(Guid id);
        Peca GetByCodigo(string codigo);
        Peca GetByCodigoFornecedor(Guid fornecedorId, string codigo);
        IEnumerable<Peca> GetAllTipoPeca();
        Paged<Peca> GetPaged(int pageNumber, int pageSize, string search);
        IEnumerable<Peca> GetToProducao(string search);
        void RemoverComponentesNaoInformados(Peca peca);
        void RemoverCodigosFornecedoresNaoInformados(Peca peca);
    }
}
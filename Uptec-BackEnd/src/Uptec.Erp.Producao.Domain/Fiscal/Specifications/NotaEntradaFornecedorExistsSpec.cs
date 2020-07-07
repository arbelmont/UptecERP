using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaEntradaFornecedorExistsSpec : IDomainSpecification<NotaEntrada>
    {
        private readonly Fornecedor _fornecedor;

        public NotaEntradaFornecedorExistsSpec(Fornecedor fornecedor)
        {
            _fornecedor = fornecedor;
        }

        public bool IsSatisfiedBy(NotaEntrada entity)
        {
            if (entity.CnpjEmissor.Numero == string.Empty) return false;

            return _fornecedor != null;
        }
    }
}

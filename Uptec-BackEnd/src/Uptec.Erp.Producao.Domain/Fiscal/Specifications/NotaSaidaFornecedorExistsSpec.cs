using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaSaidaFornecedorExistsSpec : IDomainSpecification<NotaSaida>
    {
        private readonly Fornecedor _fornecedor;

        public NotaSaidaFornecedorExistsSpec(Fornecedor fornecedor)
        {
            _fornecedor = fornecedor;
        }

        public bool IsSatisfiedBy(NotaSaida entity)
        {
            return _fornecedor != null;
        }
    }
}

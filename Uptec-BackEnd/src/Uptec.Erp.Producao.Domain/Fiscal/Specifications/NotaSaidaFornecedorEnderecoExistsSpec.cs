using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Shared.Domain.Models.Endereco;
using System.Linq;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaSaidaFornecedorEnderecoExistsSpec : IDomainSpecification<NotaSaida>
    {
        private readonly Fornecedor _fornecedor;
        private readonly Endereco _endereco;

        public NotaSaidaFornecedorEnderecoExistsSpec(Fornecedor fornecedor, Endereco endereco)
        {
            _fornecedor = fornecedor;
            _endereco = endereco;
        }

        public bool IsSatisfiedBy(NotaSaida entity)
        {
            return _fornecedor.Enderecos.FirstOrDefault(e => e.Id == _endereco.Id) != null;
        }
    }
}

using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Shared.Domain.Models.Endereco;
using System.Linq;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaSaidaClienteEnderecoExistsSpec : IDomainSpecification<NotaSaida>
    {
        private readonly Cliente _cliente;
        private readonly Endereco _endereco;

        public NotaSaidaClienteEnderecoExistsSpec(Cliente cliente, Endereco endereco)
        {
            _cliente = cliente;
            _endereco = endereco;
        }

        public bool IsSatisfiedBy(NotaSaida entity)
        {
            return _cliente.Enderecos.FirstOrDefault(e => e.Id == _endereco.Id) != null;
        }
    }
}

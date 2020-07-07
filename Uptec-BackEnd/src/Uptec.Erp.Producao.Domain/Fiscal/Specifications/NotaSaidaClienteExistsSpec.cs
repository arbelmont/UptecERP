using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaSaidaClienteExistsSpec : IDomainSpecification<NotaSaida>
    {
        private readonly Cliente _cliente;

        public NotaSaidaClienteExistsSpec(Cliente cliente)
        {
            _cliente = cliente;
        }

        public bool IsSatisfiedBy(NotaSaida entity)
        {
            return _cliente != null;
        }
    }
}

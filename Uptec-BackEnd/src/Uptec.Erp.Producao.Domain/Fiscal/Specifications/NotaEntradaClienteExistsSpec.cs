using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Clientes.Interfaces;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaEntradaClienteExistsSpec : IDomainSpecification<NotaEntrada>
    {
        private readonly Cliente _cliente;

        public NotaEntradaClienteExistsSpec(Cliente cliente)
        {
            _cliente = cliente;
        }

        public bool IsSatisfiedBy(NotaEntrada entity)
        {
            if (entity.CnpjEmissor.Numero == string.Empty) return false;

            return _cliente != null;
        }
    }
}

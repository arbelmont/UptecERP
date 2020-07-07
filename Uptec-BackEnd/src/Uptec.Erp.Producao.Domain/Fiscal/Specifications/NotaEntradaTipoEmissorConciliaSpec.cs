using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaEntradaTipoEmissorConciliaSpec : IDomainSpecification<NotaEntrada>
    {
        private readonly NotaEntrada _notaEntradaCliente;

        public NotaEntradaTipoEmissorConciliaSpec(NotaEntrada notaEntradaCliente)
        {
            _notaEntradaCliente = notaEntradaCliente;
        }

        public bool IsSatisfiedBy(NotaEntrada entity)
        {
            return _notaEntradaCliente.TipoEmissor == TipoEmissor.Cliente;
        }
    }
}

using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    public class NotaEntradaNotCliForSpec : IDomainSpecification<NotaEntrada>
    {
        public bool IsSatisfiedBy(NotaEntrada entity)
        {
            return entity.TipoEmissor == TipoEmissor.Indefinido;
        }
    }
}

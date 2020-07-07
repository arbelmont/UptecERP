using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    public class NotaEntradaNotNullSpec : IDomainSpecification<NotaEntrada>
    {
        public bool IsSatisfiedBy(NotaEntrada entity)
        {
            return entity != null;
        }
    }
}

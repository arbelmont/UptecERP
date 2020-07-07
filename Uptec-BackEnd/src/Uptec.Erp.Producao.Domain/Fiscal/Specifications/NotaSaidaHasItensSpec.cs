using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaSaidaHasItensSpec : IDomainSpecification<NotaSaida>
    {

        public NotaSaidaHasItensSpec()
        {

        }

        public bool IsSatisfiedBy(NotaSaida entity)
        {
            return entity.Itens.Count > 0;
        }
    }
}

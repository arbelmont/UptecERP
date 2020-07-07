using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaEntradaHasItensSpec : IDomainSpecification<NotaEntrada>
    {

        public NotaEntradaHasItensSpec()
        {

        }

        public bool IsSatisfiedBy(NotaEntrada entity)
        {
            return entity.Itens.Count > 0;
        }
    }
}

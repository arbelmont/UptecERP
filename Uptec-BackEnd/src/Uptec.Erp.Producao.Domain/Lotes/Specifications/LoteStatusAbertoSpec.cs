using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Lotes.Models;

namespace Uptec.Erp.Producao.Domain.Lotes.Specifications
{
    public class LoteStatusAbertoSpec : IDomainSpecification<LoteMovimento>
    {
        private readonly Lote _lote;

        public LoteStatusAbertoSpec(Lote lote)
        {
            _lote = lote;
        }

        public bool IsSatisfiedBy(LoteMovimento entity)
        {
            return _lote.Status == Shared.Domain.Enums.LoteStatus.Aberto;
        }
    }
}

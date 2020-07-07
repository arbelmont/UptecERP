using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Lotes.Models;

namespace Uptec.Erp.Producao.Domain.Lotes.Specifications
{
    public class LoteQuantidadeSuficienteSpec : IDomainSpecification<LoteMovimento>
    {
        private readonly Lote _lote;

        public LoteQuantidadeSuficienteSpec(Lote lote)
        {
            _lote = lote;
        }

        public bool IsSatisfiedBy(LoteMovimento entity)
        {
            return entity.Quantidade <= _lote.Saldo;
        }
    }
}

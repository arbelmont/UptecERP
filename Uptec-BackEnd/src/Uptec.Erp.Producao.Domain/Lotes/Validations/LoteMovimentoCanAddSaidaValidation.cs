using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Producao.Domain.Lotes.Specifications;

namespace Uptec.Erp.Producao.Domain.Lotes.Validations
{
    public class LoteMovimentoCanAddSaidaValidation : DomainValidator<LoteMovimento>
    {
        public LoteMovimentoCanAddSaidaValidation(Lote lote)
        {
            Add(new LoteQuantidadeSuficienteSpec(lote), "Quantidade insuficiente no Lote.");
            Add(new LoteStatusAbertoSpec(lote), "Lote Fechado.");
        }
    }
}

using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Producao.Domain.Lotes.Specifications;

namespace Uptec.Erp.Producao.Domain.Lotes.Validations
{
    public class LoteCanUpdateValidation : DomainValidator<Lote>
    {
        public LoteCanUpdateValidation(ILoteRepository loteRepository)
        {
        }
    }
}
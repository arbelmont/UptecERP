using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;
using Uptec.Erp.Producao.Domain.Lotes.Models;

namespace Uptec.Erp.Producao.Domain.Lotes.Validations
{
    public class LoteCanAddValidation : DomainValidator<Lote>
    {
        public LoteCanAddValidation(ILoteRepository loteRepository)
        {

        }
    }
}

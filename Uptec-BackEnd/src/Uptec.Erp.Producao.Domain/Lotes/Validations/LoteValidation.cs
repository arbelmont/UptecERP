using FluentValidation;
using Uptec.Erp.Producao.Domain.Lotes.Models;

namespace Uptec.Erp.Producao.Domain.Lotes.Validations
{
    public class LoteValidation : AbstractValidator<Lote>
    {
        public LoteValidation()
        {
        }
    }
}

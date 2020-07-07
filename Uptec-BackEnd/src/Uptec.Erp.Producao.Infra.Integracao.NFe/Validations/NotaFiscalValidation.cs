using FluentValidation;
using Uptec.Erp.Producao.Infra.Integracao.NFe.Models;

namespace Uptec.Erp.Producao.Infra.Integracao.NFe.Validations
{
    public class NotaFiscalValidation : AbstractValidator<NotaFiscal>
    {
        public NotaFiscalValidation()
        {
            //RuleFor(n => n.Items).SetValidator(new NotaFiscalItensValidation());
        }
    }
}

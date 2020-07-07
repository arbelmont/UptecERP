using FluentValidation;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Validations
{
    public class NotaSaidaValidation : AbstractValidator<NotaSaida>
    {
        public NotaSaidaValidation()
        {
            RuleForEach(n => n.Itens).SetValidator(new NotaSaidaItensValidation());
        }
    }
}
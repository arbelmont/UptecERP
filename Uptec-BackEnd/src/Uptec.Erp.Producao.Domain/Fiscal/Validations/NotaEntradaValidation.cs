using FluentValidation;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Validations
{
    public class NotaEntradaValidation : AbstractValidator<NotaEntrada>
    {
        public NotaEntradaValidation()
        {
            RuleForEach(n => n.Itens).SetValidator(new NotaEntradaItensValidation());
        }
    }
}
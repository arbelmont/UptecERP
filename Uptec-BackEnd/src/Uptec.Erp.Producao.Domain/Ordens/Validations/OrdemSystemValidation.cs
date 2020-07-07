using FluentValidation;
using System;
using Uptec.Erp.Producao.Domain.Ordens.Models;

namespace Uptec.Erp.Producao.Domain.Ordens.Validations
{
    public class OrdemSystemValidation : AbstractValidator<Ordem>
    {
        public OrdemSystemValidation()
        {
            RuleFor(o => o.DataEmissao)
                .NotNull().WithMessage("A data da emissão da ordem de produção deve ser informada.");

            RuleFor(o => o.QtdeTotal)
                .GreaterThan(0).WithMessage("Quantidade Total deve ser superior a zero.");

            RuleForEach(o => o.OrdemLotes).SetValidator(new OrdemLoteSystemValidation());
        }
    }
}
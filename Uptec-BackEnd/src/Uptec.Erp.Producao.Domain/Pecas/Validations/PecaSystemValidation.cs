using FluentValidation;
using System;
using Uptec.Erp.Producao.Domain.Pecas.Models;

namespace Uptec.Erp.Producao.Domain.Pecas.Validations
{
    public class PecaSystemValidation : AbstractValidator<Peca>
    {
        public PecaSystemValidation()
        {
            RuleFor(p => p.Id)
                .NotEqual(Guid.Empty).WithMessage("Peça não possui um identificador único.");
        }
    }
}
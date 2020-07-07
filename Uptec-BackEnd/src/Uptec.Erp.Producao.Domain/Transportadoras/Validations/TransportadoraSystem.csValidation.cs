using FluentValidation;
using System;
using Uptec.Erp.Producao.Domain.Transportadoras.Models;

namespace Uptec.Erp.Producao.Domain.Transportadoras.Validations
{
    public class TransportadoraSystemValidation : AbstractValidator<Transportadora>
    {
        public TransportadoraSystemValidation()
        {
            RuleFor(t => t.Id)
                .NotEqual(Guid.Empty).WithMessage("Transportadora não possui um identificador único.");
        }

    }
}
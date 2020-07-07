using FluentValidation;
using System;
using Uptec.Erp.Producao.Domain.Componentes.Models;

namespace Uptec.Erp.Producao.Domain.Componentes.Validations
{
    public class ComponenteSystemValidation : AbstractValidator<Componente>
    {
        public ComponenteSystemValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("Componente não possui um identificador único.");
        }
    }
}
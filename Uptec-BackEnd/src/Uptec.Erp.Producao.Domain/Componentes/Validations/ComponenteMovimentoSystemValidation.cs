using FluentValidation;
using System;
using Uptec.Erp.Producao.Domain.Componentes.Models;

namespace Uptec.Erp.Producao.Domain.Componentes.Validations
{
    public class ComponenteMovimentoSystemValidation : AbstractValidator<ComponenteMovimento>
    {
        public ComponenteMovimentoSystemValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("ComponenteMovimento não possui um identificador único.");

            RuleFor(c => c.PrecoTotal)
                .GreaterThan(0).WithMessage("Impossível determinar o preço total.");
        }
    }
}
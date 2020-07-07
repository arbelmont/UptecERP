using FluentValidation;
using System;
using Uptec.Erp.Producao.Domain.Clientes.Models;

namespace Uptec.Erp.Producao.Domain.Clientes.Validations
{
    public class ClienteSystemValidation : AbstractValidator<Cliente>
    {
        public ClienteSystemValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("Cliente não possui um identificador único.");
        }
    }
}
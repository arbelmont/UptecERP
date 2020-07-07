using FluentValidation;
using System;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;

namespace Uptec.Erp.Producao.Domain.Fornecedores.Validations
{
    public class FornecedorSystemValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorSystemValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage("Fornecedor não possui um identificador único.");
        }
    }
}
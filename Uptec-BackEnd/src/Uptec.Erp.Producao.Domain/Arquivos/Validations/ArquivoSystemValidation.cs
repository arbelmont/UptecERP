using FluentValidation;
using System;
using Uptec.Erp.Producao.Domain.Arquivos.Models;

namespace Uptec.Erp.Producao.Domain.Arquivos.Validations
{
    public class ArquivoSystemValidation : AbstractValidator<Arquivo>
    {
        public ArquivoSystemValidation()
        {
            RuleFor(f => f.Id)
                .NotEqual(Guid.Empty).WithMessage("Arquivo não possui um identificador unico.");
        }
    }
}
using FluentValidation;
using System;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Shared.Domain.Models.Cfop;

namespace Uptec.Erp.Producao.Domain.Fiscal.Validations
{
    public class NotaSaidaItensSystemValidation : AbstractValidator<NotaSaidaItens>
    {
        public NotaSaidaItensSystemValidation()
        {

            //RuleFor(n => n.Id)
            //    .NotEqual(Guid.Empty).WithMessage("Item não possui um identificador unico.");

            RuleFor(n => n.Codigo)
                .NotEmpty().WithMessage("Código do item inválido")
                .NotNull().WithMessage("Código do item inválido");

            RuleFor(n => n.ValorUnitario)
                .GreaterThan(0).WithMessage("Valor unitário do item inválido");

            RuleFor(n => n.Quantidade)
                .GreaterThan(0).WithMessage("Quantidade do item inválida");

            RuleFor(n => n.ValorTotal)
                .GreaterThan(0).WithMessage("Valor total do item não calculado");

            RuleFor(n => n.Cfop)
                .MaximumLength(NotaSaida.CfopMaxLenght).WithMessage("Cfop - Quantidade de caracteres excedida")
                .NotEmpty().WithMessage("Cfop inválido")
                .NotNull().WithMessage("Cfop inválido");

            RuleFor(n => n.Ncm)
                .MaximumLength(NotaSaidaItens.NcmMaxLenght).WithMessage("Ncm - Quantidade de caracteres excedida")
                .NotEmpty().WithMessage("Ncm inválido")
                .NotNull().WithMessage("Ncm inválido");


        }
    }
}
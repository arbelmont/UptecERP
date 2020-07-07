using FluentValidation;
using System;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Validations
{
    public class NotaEntradaItensSystemValidation : AbstractValidator<NotaEntradaItens>
    {
        public NotaEntradaItensSystemValidation()
        {

            //RuleFor(n => n.Id)
            //    .NotEqual(Guid.Empty).WithMessage("Item não possui um identificador unico.");

            RuleFor(n => n.Codigo)
                .NotEmpty().WithMessage("Código do item inválido")
                .NotNull().WithMessage("Código do item inválido");

            RuleFor(n => n.PrecoUnitario)
                .GreaterThan(0).WithMessage("Preço unitário do item inválido");

            RuleFor(n => n.Quantidade)
                .GreaterThan(0).WithMessage("Quantidade do item inválida");

            RuleFor(n => n.PrecoTotal)
                .GreaterThan(0).WithMessage("Preço total do item não calculado");

            RuleFor(n => n.Cfop)
                .NotEmpty().WithMessage("Cfop inválido")
                .NotNull().WithMessage("Cfop inválido");

            RuleFor(n => n.NumeroNotaCobertura)
                .MaximumLength(NotaEntradaItens.NumeroNotaMaxLenght)
                .WithMessage($"Numero de Nota aceita no máximo {NotaEntradaItens.NumeroNotaMaxLenght} caracteres.");

            RuleFor(n => n.Localizacao)
                .MaximumLength(NotaEntradaItens.LocalizacaoMaxLenght);
        }
    }
}
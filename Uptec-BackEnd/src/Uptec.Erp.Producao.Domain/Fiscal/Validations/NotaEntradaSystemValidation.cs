using FluentValidation;
using System;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Shared.Domain.Models.Cfop;

namespace Uptec.Erp.Producao.Domain.Fiscal.Validations
{
    public class NotaEntradaSystemValidation : AbstractValidator<NotaEntrada>
    {
        public NotaEntradaSystemValidation()
        {

            RuleFor(n => n.Id)
                .NotEqual(Guid.Empty).WithMessage("Nota não possui um identificador unico.");

            RuleFor(n => n.NumeroNota)
                .NotEmpty().WithMessage("NumeroNota inválido")
                .NotNull().WithMessage("NumeroNota inválido")
                .MaximumLength(NotaEntrada.NumeroNotaMaxLenght).WithMessage("NumeroNota - Quantidade de caracteres excedida");

            RuleFor(n => n.NomeEmissor)
                .NotEmpty().WithMessage("Nome emissor inválido")
                .NotNull().WithMessage("Nome emissor inválido")
                .MaximumLength(NotaEntrada.NomeEmissorMaxLenght).WithMessage("NomeEmissor - Quantidade de caracteres excedida");

            RuleFor(n => n.CnpjEmissor)
                .NotNull().WithMessage("Cnpj emissor inválido.");

            RuleFor(n => n.EmailEmissor)
                .NotNull().WithMessage("Email emissor inválido.");
                // .Must(n =>n.EhValido).WithMessage("Email emissor inválido."); TODO (by Marcone) gerando erro

            RuleFor(n => n.Valor)
                .GreaterThan(0).WithMessage("Valor inválido");

            RuleFor(n => n.Cfop)
                .Must(c => CfopUptec.CfopsEntrada.Contains(c)).WithMessage("Cfop não tratado no sistema")
                .NotEmpty().WithMessage("Cfop inválido")
                .NotNull().WithMessage("Cfop inválido")
                .MaximumLength(NotaEntrada.CfopMaxLenght).WithMessage("Cfop - Quantidade de caracteres excedida");

            RuleForEach(n => n.Itens).SetValidator(new NotaEntradaItensSystemValidation());
        }
    }
}
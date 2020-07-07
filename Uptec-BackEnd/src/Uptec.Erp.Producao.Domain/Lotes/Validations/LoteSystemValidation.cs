using FluentValidation;
using System;
using Uptec.Erp.Producao.Domain.Lotes.Models;

namespace Uptec.Erp.Producao.Domain.Lotes.Validations
{
    public class LoteSystemValidation : AbstractValidator<Lote>
    {
        public LoteSystemValidation()
        {
            RuleFor(l => l.Data)
                .NotNull().WithMessage("A data de geração do lote deve ser informada.");

            //RuleFor(l => l.LoteNumero)
            //    .GreaterThan(0).WithMessage("Número do lote deve ser superior a zero");

            //RuleFor(l => l.NotaEntradaId) //TODO: Descomentar e testar quando tivermos nota
            //    .NotEqual(Guid.Empty).WithMessage("Nenhuma Nota Fiscal de Entrada foi informada.");

            RuleFor(l => l.PecaId)
                .NotEqual(Guid.Empty).WithMessage("Nenhuma Peça foi informada.");

            RuleFor(l => l.Quantidade)
                .GreaterThan(0).WithMessage("Quantidade deve ser superior a zero.");

            RuleFor(l => l.Saldo)
                .GreaterThanOrEqualTo(0).WithMessage("Saldo não pode ser inferior a zero.");

            RuleFor(l => l.PrecoEntrada)
                .GreaterThan(0).WithMessage("Preço de entrada deve ser superior a zero.");

            RuleFor(l => l.NotaFiscal)
                .NotNull().NotEmpty().WithMessage("Nenhuma Nota Fiscal de Entrada foi informada.");
            
            RuleFor(l => l.Localizacao)
                .MaximumLength(Lote.LocalizacaoMaxLenght)
                .WithMessage($"Localização do Lote pode conter no máximo {Lote.LocalizacaoMaxLenght} caracteres.");
        }
    }
}
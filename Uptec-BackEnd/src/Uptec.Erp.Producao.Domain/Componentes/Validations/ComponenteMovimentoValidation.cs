using FluentValidation;
using Uptec.Erp.Producao.Domain.Componentes.Models;

namespace Uptec.Erp.Producao.Domain.Componentes.Validations
{
    public class ComponenteMovimentoValidation : AbstractValidator<ComponenteMovimento>
    {
        public ComponenteMovimentoValidation()
        {
            RuleFor(c => c.Quantidade)
                .GreaterThan(0).WithMessage("Quantidade não informada.");

            RuleFor(c => c.PrecoUnitario)
                .GreaterThan(0).WithMessage("Preço unitário não informada.");

            RuleFor(c => c.NotaFiscal)
                .MaximumLength(ComponenteMovimento.NotaFiscalMaxLenght)
                .WithMessage($"Nota fiscal aceita no máximo {ComponenteMovimento.NotaFiscalMaxLenght} caracteres.");

            RuleFor(c => c.Historico)
                .NotEmpty().WithMessage("Obrigatório o preenchimento do Histórico.")
                .MaximumLength(ComponenteMovimento.HistoricoMaxLenght)
                .WithMessage($"Histórico aceita no máximo {ComponenteMovimento.HistoricoMaxLenght} caracteres.");

        }
    }
}
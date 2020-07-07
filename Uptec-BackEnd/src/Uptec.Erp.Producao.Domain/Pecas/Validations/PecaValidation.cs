using FluentValidation;
using System.Linq;
using Uptec.Erp.Producao.Domain.Pecas.Models;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Pecas.Validations
{
    public class PecaValidation : AbstractValidator<Peca>
    {
        public PecaValidation()
        {
            RuleFor(p => p.Codigo)
                .NotEmpty().WithMessage("Obrigatório o preenchimento do Código.")
                .MaximumLength(Peca.CodigoMaxLenght)
                    .WithMessage($"Código aceita no máximo {Peca.CodigoMaxLenght} caracteres.");

            RuleFor(p => p.Descricao)
                .NotEmpty().WithMessage("Necessário preencher o Descrição.")
                .MaximumLength(Peca.DescricaoMaxLenght)
                    .WithMessage($"Descrição aceita no máximo {Peca.DescricaoMaxLenght} caracteres.");

            RuleFor(p => p.Unidade)
                .NotNull().WithMessage("O campo Unidade precisa ser informado.");

            RuleFor(p => p.Preco)
                .GreaterThan(0).WithMessage("Preço deve ser superior a zero.");
            RuleFor(p => p.PrecoSaida)
                .GreaterThan(0).WithMessage("Preço de Saída deve ser superior a zero.");

            RuleFor(p => p.Ncm)
                .NotNull().WithMessage("Obrigatório o preenchimento do NCM.")
                .MaximumLength(Peca.NcmMaxLenght)
                    .WithMessage($"NCM aceita no máximo {Peca.NcmMaxLenght} caracteres.");

            RuleFor(p => p.CodigoSaida)
                .MaximumLength(Peca.CodigoSaidaMaxLenght)
                    .WithMessage($"Códido de Saída aceita no máximo {Peca.CodigoMaxLenght} caracteres.")
                    .When(p => p.CodigoSaida != null);

            RuleFor(p => p.Revisao)
                .MaximumLength(Peca.RevisaoMaxLenght)
                    .WithMessage($"Revisão aceita no máximo {Peca.RevisaoMaxLenght} caracteres.")
                    .When(p => p.Revisao != null);

            RuleFor(p => p.Componentes)
                .Must(c => c.Any()).WithMessage("Ao menos um componente deve ser informado para a peça.")
                .When(p => p.Tipo == TipoPeca.Peca);
        }
    }
}
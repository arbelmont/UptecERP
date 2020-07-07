using FluentValidation;
using Uptec.Erp.Producao.Domain.Componentes.Models;

namespace Uptec.Erp.Producao.Domain.Componentes.Validations
{
    public class ComponenteValidation : AbstractValidator<Componente>
    {
        public ComponenteValidation()
       {
            RuleFor(p => p.Codigo)
                .NotEmpty().WithMessage("Obrigatório o preenchimento do Código.")
                .MaximumLength(Componente.CodigoMaxLenght)
                    .WithMessage($"Código aceita no máximo {Componente.CodigoMaxLenght} caracteres.");

            RuleFor(p => p.Descricao)
                .NotEmpty().WithMessage("Necessário preencher a Descrição.")
                .MaximumLength(Componente.DescricaoMaxLenght)
                    .WithMessage($"Descrição aceita no máximo {Componente.DescricaoMaxLenght} caracteres.");

            RuleFor(p => p.Unidade)
                .NotNull().WithMessage("O campo Unidade precisa ser informado.");

            //RuleFor(p => p.Preco)
                //.GreaterThan(0).WithMessage("Preço deve ser superior a zero.");

            RuleFor(p => p.Ncm)
                .NotNull().WithMessage("Obrigatório o preenchimento do NCM.")
                .MaximumLength(Componente.NcmMaxLenght)
                    .WithMessage($"NCM aceita no máximo {Componente.NcmMaxLenght} caracteres.");
        }
    }
}
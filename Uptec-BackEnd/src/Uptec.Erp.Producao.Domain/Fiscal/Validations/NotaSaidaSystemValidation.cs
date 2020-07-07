using FluentValidation;
using System;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Validations
{
    public class NotaSaidaSystemValidation : AbstractValidator<NotaSaida>
    {
        public NotaSaidaSystemValidation()
        {

            RuleFor(n => n.Id)
                .NotEqual(Guid.Empty).WithMessage("Nota não possui um identificador unico.");

            RuleFor(n => n.NaturezaOperacao)
                .MaximumLength(NotaSaida.NaturezaOperacaoMaxLenght).WithMessage("Natureza da operação ultrapassou o limite de caracteres permitidos");

            //RuleFor(n => n.NumeroNota)
            //    .NotEmpty().WithMessage("NumeroNota inválido")
            //    .NotNull().WithMessage("NumeroNota inválido")
            //    .MaximumLength(NotaSaida.NumeroNotaMaxLenght).WithMessage("NumeroNota - Quantidade de caracteres excedida");

            RuleFor(n => n.ValorTotalNota)
                .GreaterThan(0).WithMessage("Valor total da nota inválido");

            RuleFor(n => n.ValorTotalProdutos)
                .GreaterThan(0).WithMessage("Valor total dos produtos inválido");

            RuleFor(n => n.ErroApi)
                .MaximumLength(NotaSaida.ErroApiMaxLenght).WithMessage("Descrição do Erro da api ultrapassou o limite de caracteres permitidos");

            RuleFor(n => n.OutrasInformacoes)
                .MaximumLength(NotaSaida.OutrasInformacoesMaxLenght).WithMessage("Outras informações ultrapassou o limite de caracteres permitidos");

            RuleForEach(n => n.Itens).SetValidator(new NotaSaidaItensSystemValidation());
        }
    }
}
using System;
using FluentValidation;

namespace Uptec.Erp.Shared.Domain.Models.Endereco.Validations
{
    public class EnderecoValidation : AbstractValidator<Endereco>
    {
        public EnderecoValidation(bool obrigatorio)
        {
            if (obrigatorio)
            {
                RuleFor(e => e.Id)
                    .NotEqual(Guid.Empty).WithMessage("Endereço não possui um identificador único.");

                RuleFor(e => e.Logradouro)
                    .MaximumLength(Endereco.LogradouroMaxLength).WithMessage($"Logradouro aceita no máximo {Endereco.LogradouroMaxLength} caracteres.")
                    .NotEmpty().WithMessage("Endereço incompleto, necessário informar o Logradouro.");

                RuleFor(e => e.Numero)
                    .MaximumLength(Endereco.NumeroMaxLength).WithMessage($"Numero do Endereço aceita no máximo {Endereco.NumeroMaxLength} caracteres.")
                    .NotEmpty().WithMessage("Endereço incompleto, necessário informar o Número.");

                RuleFor(e => e.Cidade)
                    .NotNull().WithMessage("Endereço incompleto, necessário informar a Cidade.");

                RuleFor(e => e.Estado)
                    .NotNull().WithMessage("Endereço incompleto, necessário informar o Estado.");
            }
            else
            {
                RuleFor(e => e.Logradouro)
                    .MaximumLength(Endereco.LogradouroMaxLength)
                            .WithMessage($"Logradouro aceita no máximo {Endereco.LogradouroMaxLength} caracteres.");

                RuleFor(e => e.Numero)
                    .MaximumLength(Endereco.NumeroMaxLength)
                            .WithMessage($"Numero do Endereço aceita no máximo {Endereco.NumeroMaxLength} caracteres.");

                RuleFor(e => e.Cidade)
                        .NotNull().WithMessage("Necessário informar a Cidade.");

                RuleFor(e => e.Estado)
                    .NotNull().WithMessage("Necessário informar o Estado.");
            }
            

            RuleFor(e => e.Complemento)
                .MaximumLength(Endereco.ComplementoMaxLength)
                .WithMessage($"Complemento do Endereço aceita no máximo {Endereco.ComplementoMaxLength} caracteres."); ;

            RuleFor(e => e.Bairro)
                .MaximumLength(Endereco.BairroMaxLength)
                .WithMessage($"Bairro aceita no máximo {Endereco.BairroMaxLength} caracteres."); ;

            RuleFor(e => e.Cep)
                .MaximumLength(Endereco.CepMaxLength)
                .WithMessage($"Cep inválido.");
        }
    }
}
using System;
using FluentValidation;

namespace Uptec.Erp.Shared.Domain.Models.Telefone.Validations
{
    public class TelefoneValidation : AbstractValidator<Telefone>
    {
        public TelefoneValidation(bool obrigatorio)
        {
            if (obrigatorio)
            {
                RuleFor(e => e.Id)
                    .NotEqual(Guid.Empty).WithMessage("Telefone não possui um identificador único.");

                RuleFor(t => t.Numero)
                    .NotEmpty().WithMessage("Número do Telefone deve ser informado.")
                    .MaximumLength(Telefone.NumeroMaxLength)
                            .WithMessage($"Número do Telefone aceita no máximo {Telefone.NumeroMaxLength} caracteres.");
            }
            else
            {
                RuleFor(t => t.Numero)
                    .MaximumLength(Telefone.NumeroMaxLength)
                            .WithMessage($"Número do Telefone aceita no máximo {Telefone.NumeroMaxLength} caracteres.");
            }

            RuleFor(t => t.Observacoes)
                .MaximumLength(Telefone.ObservacoesMaxLength)
                            .WithMessage($"Observações aceita no máximo {Telefone.ObservacoesMaxLength} caracteres.");
            RuleFor(t => t.Contato)
                .MaximumLength(Telefone.ContatoMaxLength)
                            .WithMessage($"Contato aceita no máximo {Telefone.ContatoMaxLength} caracteres.");
        }
    }
}
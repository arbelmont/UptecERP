using Definitiva.Shared.Infra.Support.Helpers;
using FluentValidation;
using Uptec.Erp.Producao.Domain.Transportadoras.Models;
using Uptec.Erp.Shared.Domain.Models.Endereco.Validations;
using Uptec.Erp.Shared.Domain.Models.Telefone.Validations;

namespace Uptec.Erp.Producao.Domain.Transportadoras.Validations
{
    public class TransportadoraValidation : AbstractValidator<Transportadora>
    {
        public TransportadoraValidation()
        {

            RuleFor(n => n.Cnpj)
                .NotNull()
                .Must(t => t.EhValido).WithMessage("Cnpj inválido.");

            RuleFor(t => t.InscricaoEstadual.GetOnlyNumbers())
                .MaximumLength(Transportadora.InscricaoEstadualMaxLength)
                .WithMessage($"Inscrição Estadual aceita no máximo {Transportadora.InscricaoEstadualMaxLength} caracteres.");

            RuleFor(t => t.RazaoSocial)
                .NotNull()
                .MaximumLength(Transportadora.RazaoSocialMaxLength)
                .WithMessage($"Razão Social aceita no máximo {Transportadora.InscricaoEstadualMaxLength} caracteres.");

            RuleFor(t => t.NomeFantasia)
                .NotNull()
                .MaximumLength(Transportadora.NomeFantasiaMaxLength)
                .WithMessage($"Nome Fantasia aceita máximo {Transportadora.InscricaoEstadualMaxLength} caracteres.");

            RuleFor(c => c.Email)
                .NotNull()
                .Must(e => e.EhValido).WithMessage("Email inválido.");

            RuleFor(t => t.Observacoes)
                .MaximumLength(Transportadora.RazaoSocialMaxLength)
                .WithMessage($"Oberservações aceita no máximo {Transportadora.ObservacoesMaxLength} caracteres.");

            RuleFor(t => t.Endereco).SetValidator(new EnderecoValidation(TransportadoraEndereco.EnderecoObrigatorio));
            RuleFor(t => t.Telefone).SetValidator(new TelefoneValidation(TransportadoraTelefone.TelefoneObrigatorio));

        }

    }
}
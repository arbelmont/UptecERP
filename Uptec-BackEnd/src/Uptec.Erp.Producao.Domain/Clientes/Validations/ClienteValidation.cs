using Definitiva.Shared.Infra.Support.Helpers;
using FluentValidation;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Shared.Domain.Models.Endereco.Validations;
using Uptec.Erp.Shared.Domain.Models.Telefone.Validations;

namespace Uptec.Erp.Producao.Domain.Clientes.Validations
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(n => n.Cnpj)
                .NotNull()
                .Must(t => t.EhValido).WithMessage("Cnpj inválido.");

            RuleFor(t => t.InscricaoEstadual.GetOnlyNumbers())
                .MaximumLength(Cliente.InscricaoEstadualMaxLength)
                .WithMessage($"Inscrição Estadual aceita no máximo {Cliente.InscricaoEstadualMaxLength} caracteres.");

            RuleFor(t => t.RazaoSocial)
                .NotNull()
                .MaximumLength(Cliente.RazaoSocialMaxLength)
                .WithMessage($"Razão Social aceita no máximo {Cliente.InscricaoEstadualMaxLength} caracteres.");

            RuleFor(t => t.NomeFantasia)
                .NotNull()
                .MaximumLength(Cliente.NomeFantasiaMaxLength)
                .WithMessage($"Nome Fantasia aceita máximo {Cliente.InscricaoEstadualMaxLength} caracteres.");

            RuleFor(c => c.Email)
                .NotNull()
                .Must(e => e.EhValido).WithMessage("Email inválido.");

            RuleFor(t => t.Observacoes)
                .MaximumLength(Cliente.RazaoSocialMaxLength)
                .WithMessage($"Oberservações aceita no máximo {Cliente.ObservacoesMaxLength} caracteres.");

            RuleFor(t => t.Endereco).SetValidator(new EnderecoValidation(ClienteEndereco.EnderecoObrigatorio));
            RuleFor(t => t.Telefone).SetValidator(new TelefoneValidation(ClienteTelefone.TelefoneObrigatorio));
        }
    }
}
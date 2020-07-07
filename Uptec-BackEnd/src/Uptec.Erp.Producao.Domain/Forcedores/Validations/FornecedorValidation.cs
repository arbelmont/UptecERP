using Definitiva.Shared.Infra.Support.Helpers;
using FluentValidation;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Shared.Domain.Models.Endereco.Validations;
using Uptec.Erp.Shared.Domain.Models.Telefone.Validations;

namespace Uptec.Erp.Producao.Domain.Fornecedores.Validations
{
    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(n => n.Cnpj)
                .NotNull()
                .Must(t => t.EhValido).WithMessage("Cnpj inválido.");

            RuleFor(t => t.InscricaoEstadual.GetOnlyNumbers())
                .MaximumLength(Fornecedor.InscricaoEstadualMaxLength)
                .WithMessage($"Inscrição Estadual aceita no máximo {Fornecedor.InscricaoEstadualMaxLength} caracteres.");

            RuleFor(t => t.RazaoSocial)
                .NotNull()
                .MaximumLength(Fornecedor.RazaoSocialMaxLength)
                .WithMessage($"Razão Social aceita no máximo {Fornecedor.InscricaoEstadualMaxLength} caracteres.");

            RuleFor(t => t.NomeFantasia)
                .NotNull()
                .MaximumLength(Fornecedor.NomeFantasiaMaxLength)
                .WithMessage($"Nome Fantasia aceita máximo {Fornecedor.InscricaoEstadualMaxLength} caracteres.");

            RuleFor(c => c.Email)
                .NotNull()
                .Must(e => e.EhValido).WithMessage("Email inválido.");

            RuleFor(t => t.Observacoes)
                .MaximumLength(Fornecedor.RazaoSocialMaxLength)
                .WithMessage($"Oberservações aceita no máximo {Fornecedor.ObservacoesMaxLength} caracteres.");

            RuleFor(t => t.Endereco).SetValidator(new EnderecoValidation(FornecedorEndereco.EnderecoObrigatorio));
            RuleFor(t => t.Telefone).SetValidator(new TelefoneValidation(FornecedorTelefone.TelefoneObrigatorio));
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using CnpjAlias = Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf.Cnpj;

namespace Uptec.Erp.Api.ViewModels.Producao.Fornecedores
{
    public class FornecedorViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(CnpjAlias.MaxLength + 4, ErrorMessage = "Cnpj inválido.")]
        public string Cnpj { get; set; }

        [MaxLength(Fornecedor.InscricaoEstadualMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string InscricaoEstadual { get; set; }

        [Required]
        [MaxLength(Fornecedor.RazaoSocialMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string RazaoSocial { get; set; }

        [Required]
        [MaxLength(Fornecedor.NomeFantasiaMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string NomeFantasia { get; set; }


        public FornecedorEnderecoViewModel Endereco { get; set; }
        public FornecedorTelefoneViewModel Telefone { get; set; }

        [MaxLength(Erp.Shared.Domain.ValueObjects.Email.MaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Email { get; set; }

        [MaxLength(Fornecedor.WebsiteMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Website { get; set; }

        [MaxLength(Fornecedor.ObservacoesMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Observacoes { get; set; }

        public List<FornecedorEnderecoViewModel> Enderecos { get; set; }
    }
}

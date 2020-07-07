using System;
using System.ComponentModel.DataAnnotations;
using Uptec.Erp.Producao.Domain.Transportadoras.Models;
using Uptec.Erp.Shared.Domain.Enums;
using CnpjAlias = Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf.Cnpj;

namespace Uptec.Erp.Api.ViewModels.Producao.Transportadoras
{
    public class TransportadoraViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(CnpjAlias.MaxLength + 4, ErrorMessage = "Cnpj inválido.")]
        public string Cnpj { get; set; }

        [MaxLength(Transportadora.InscricaoEstadualMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string InscricaoEstadual { get; set; }

        [Required]
        [MaxLength(Transportadora.RazaoSocialMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string RazaoSocial { get; set; }

        [Required]
        [MaxLength(Transportadora.NomeFantasiaMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string NomeFantasia { get; set; }


        public TransportadoraEnderecoViewModel Endereco { get; set; }
        public TransportadoraTelefoneViewModel Telefone { get; set; }

        [MaxLength(Erp.Shared.Domain.ValueObjects.Email.MaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Email { get; set; }

        [MaxLength(Transportadora.WebsiteMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Website { get; set; }

        [Required]
        public TransportadoraTipoEntregaPadrao TipoEntregaPadrao { get; set; }

        [MaxLength(Transportadora.ObservacoesMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Observacoes { get; set; }
    }
}
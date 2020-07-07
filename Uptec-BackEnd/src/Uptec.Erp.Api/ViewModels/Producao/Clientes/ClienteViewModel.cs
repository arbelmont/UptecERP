using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Uptec.Erp.Api.ViewModels.Producao.Transportadoras;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using CnpjAlias = Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf.Cnpj;

namespace Uptec.Erp.Api.ViewModels.Producao.Clientes
{
    public class ClienteViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? TransportadoraId { get; set; }

        [Required]
        [MaxLength(CnpjAlias.MaxLength + 4, ErrorMessage = "Cnpj inválido.")]
        public string Cnpj { get; set; }

        [MaxLength(Cliente.InscricaoEstadualMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string InscricaoEstadual { get; set; }

        [Required]
        [MaxLength(Cliente.RazaoSocialMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string RazaoSocial { get; set; }

        [Required]
        [MaxLength(Cliente.NomeFantasiaMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string NomeFantasia { get; set; }


        public ClienteEnderecoViewModel Endereco { get; set; }
        public ClienteTelefoneViewModel Telefone { get; set; }

        [MaxLength(Erp.Shared.Domain.ValueObjects.Email.MaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Email { get; set; }

        [MaxLength(Cliente.WebsiteMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Website { get; set; }

        [MaxLength(Cliente.ObservacoesMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Observacoes { get; set; }

        public List<ClienteEnderecoViewModel> Enderecos { get; set; }
        public TransportadoraViewModel Transportadora { get; set; }
    }
}

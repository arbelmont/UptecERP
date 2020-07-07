using System;
using System.ComponentModel.DataAnnotations;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Telefone;

namespace Uptec.Erp.Api.ViewModels.Producao.Clientes
{
    public class ClienteTelefoneViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ClienteId { get; set; }

        [MaxLength(Telefone.NumeroMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Numero { get; set; }

        public TelefoneTipo Tipo { get; set; }

        public bool Whatsapp { get; set; }

        [MaxLength(Telefone.ObservacoesMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Observacoes { get; set; }

        [MaxLength(Telefone.ContatoMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Contato { get; set; }

        public bool Obrigatorio { get; set; }
    }
}

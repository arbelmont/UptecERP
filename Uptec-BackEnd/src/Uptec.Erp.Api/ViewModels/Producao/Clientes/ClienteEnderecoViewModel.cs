using System;
using System.ComponentModel.DataAnnotations;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Endereco;
using CidadeAlias = Uptec.Erp.Shared.Domain.ValueObjects.Cidade;
using EstadoAlias = Uptec.Erp.Shared.Domain.ValueObjects.Estado;

namespace Uptec.Erp.Api.ViewModels.Producao.Clientes
{
    public class ClienteEnderecoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid ClienteId { get; set; }

        [MaxLength(Endereco.LogradouroMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Logradouro { get; set; }

        [MaxLength(Endereco.NumeroMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Numero { get; set; }

        [MaxLength(Endereco.ComplementoMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Complemento { get; set; }

        [MaxLength(Endereco.BairroMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Bairro { get; set; }

        [MaxLength(Endereco.CepMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Cep { get; set; }

        [MaxLength(CidadeAlias.NomeCidadeMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe a Cidade.")]
        public string Cidade { get; set; }

        [MaxLength(EstadoAlias.SiglaMaxLength, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe o Estado.")]
        public string Estado { get; set; }

        public EnderecoTipo Tipo { get; set; }

        public bool Obrigatorio { get; set; }
    }
}

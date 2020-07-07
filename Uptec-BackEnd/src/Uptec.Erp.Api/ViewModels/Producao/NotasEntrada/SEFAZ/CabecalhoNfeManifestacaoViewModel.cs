using System;
using System.ComponentModel.DataAnnotations;
using Uptec.Erp.Shared.Domain.Enums.NFe.Processamento;

namespace Uptec.Erp.Api.ViewModels.Producao.NotasEntrada.SEFAZ
{
    public class CabecalhoNfeManifestacaoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(ChaveNfeLenght, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Informe a chave da Nota Fiscal.")]
        public string ChaveNfe { get; set; }

        [Required(ErrorMessage = "Status da Manifestaçação deve ser informado")]
        public ManifestacaoStatus? ManifestacaoDestinatario { get; set; }

        public string Justificativa { get; set; }

        public const byte ChaveNfeLenght = 50;
        public const byte JustificativaManifestacaoMinLenght = 15;
        public const byte JustificativaManifestacaoMaxLenght = 255;
    }
}

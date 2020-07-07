using System;
using System.ComponentModel.DataAnnotations;
using Uptec.Erp.Shared.Domain.Enums;
using ComponenteAlias = Uptec.Erp.Producao.Domain.Componentes.Models.Componente;


namespace Uptec.Erp.Api.ViewModels.Producao.Componente
{
    public class ComponenteViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(ComponenteAlias.CodigoMaxLenght, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Codigo { get; set; }

        [Required]
        [MaxLength(ComponenteAlias.DescricaoMaxLenght, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Descricao { get; set; }

        [Required]
        public UnidadeMedida Unidade { get; set; }

        [Required]
        public decimal Preco { get; set; }

        [Required]
        [MaxLength(ComponenteAlias.NcmMaxLenght, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Ncm { get; set; }

        public decimal Quantidade { get; set; }

        public decimal QuantidadeMinima { get; set; }
    }
}

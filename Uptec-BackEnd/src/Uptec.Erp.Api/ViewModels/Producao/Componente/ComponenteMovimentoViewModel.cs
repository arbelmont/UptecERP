using System;
using System.ComponentModel.DataAnnotations;
using Uptec.Erp.Producao.Domain.Componentes.Models;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.Componente
{
    public class ComponenteMovimentoViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ComponenteId { get; set; }

        [Required]
        public decimal Quantidade { get; set; }

        [Required]
        public DateTime Data { get; set; }

        [Required]
        public TipoMovimentoEstoque TipoMovimento { get; set; }

        [Required]
        public decimal PrecoUnitario { get; set; }

        public decimal PrecoTotal { get; set; }

        [MaxLength(ComponenteMovimento.NotaFiscalMaxLenght, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string NotaFiscal { get; set; } 

        public decimal Saldo { get; set; }

        [Required]
        [MaxLength(ComponenteMovimento.HistoricoMaxLenght, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Historico { get; set; }

        public virtual ComponenteViewModel Componente { get; set; }

    }
}

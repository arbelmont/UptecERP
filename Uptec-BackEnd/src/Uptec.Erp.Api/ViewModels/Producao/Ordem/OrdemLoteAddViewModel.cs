using System;
using System.ComponentModel.DataAnnotations;

namespace Uptec.Erp.Api.ViewModels.Producao.Ordem
{
    public class OrdemLoteAddViewModel
    {
        [Required]
        public Guid LoteId { get; set; }

        [Required]
        public int LoteNumero { get; set; }

        [Required]
        public int LoteSequencia { get; set; }

        [Required]
        public decimal Qtde { get; set; }
    }
}

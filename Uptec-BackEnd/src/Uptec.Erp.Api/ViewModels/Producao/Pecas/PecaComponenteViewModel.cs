using System;
using System.ComponentModel.DataAnnotations;

namespace Uptec.Erp.Api.ViewModels.Producao.Pecas
{
    public class PecaComponenteViewModel
    {
        public Guid PecaId { get; set; }

        [Required]
        public Guid ComponenteId { get; set; }

        [Required]
        public decimal Quantidade { get; set; }
    }
}

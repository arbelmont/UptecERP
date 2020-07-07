using System;
using System.ComponentModel.DataAnnotations;

namespace Uptec.Erp.Api.ViewModels.Producao.Pecas
{
    public class PecaFornecedorCodigoViewModel
    {
        public Guid PecaId { get; set; }

        [Required]
        public Guid FornecedorId { get; set; }

        [Required]
        public string FornecedorCodigo { get; set; }
    }
}

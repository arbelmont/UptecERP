using System;
using System.ComponentModel.DataAnnotations;

namespace Uptec.Erp.Api.ViewModels.Producao.NotasEntrada
{
    public class NotaEntradaCoberturaViewModel
    {
        [Required]
        public Guid NotaEntradaFornecedorId { get; set; }
        [Required]
        public Guid NotaEntradaClienteId { get; set; }
    }
}

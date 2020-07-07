using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Uptec.Erp.Api.ViewModels.Producao.Arquivos
{
    public class ArquivoAddViewModel
    {
        [Required]
        public IFormFile Dados { get; set; }
    }
}

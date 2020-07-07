using System.Collections.Generic;

namespace Uptec.Erp.Api.ViewModels.Producao.Ordem
{
    public class OrdemAddViewModel
    {
        public decimal QtdeTotal { get; set; }
        public List<OrdemLoteAddViewModel> OrdemLotes { get; set; }
    }
}

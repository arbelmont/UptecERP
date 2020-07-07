using System;

namespace Uptec.Erp.Api.ViewModels.Producao.Lotes
{
    public class LoteSaldoViewModel
    {
        public Guid Id { get; set; }
        public string Cliente { get; set; }
        public string Codigo { get; set; }
        public string Produto { get; set; }
        public decimal? Entrada { get; set; }
        public decimal? Saldo { get; set; }
    }
}

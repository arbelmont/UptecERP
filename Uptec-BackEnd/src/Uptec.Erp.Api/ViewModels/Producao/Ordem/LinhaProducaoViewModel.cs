
namespace Uptec.Erp.Api.ViewModels.Producao.Ordem
{
    public class LinhaProducaoViewModel
    {
        public string Cliente { get; set; }
        public string Materia { get; set; }
        public string Produto { get; set; }
        public decimal? Entrada { get; set; }
        public decimal? Saldo { get; set; }
        public decimal? Producao { get; set; }
        public decimal? Expedicao { get; set; }
    }
}

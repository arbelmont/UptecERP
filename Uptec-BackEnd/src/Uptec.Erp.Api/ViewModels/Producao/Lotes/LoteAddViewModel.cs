using System;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.Lotes
{
    public class LoteAddViewModel
    {
        public int LoteNumero { get; set; }
        public Guid PecaId { get; set; }
        public TipoPeca TipoPeca { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoEntrada { get; set; }
        public string CfopEntrada { get; set; }
        public string NotaFiscal { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public string Localizacao { get; set; }
        public decimal QtdeConcilia { get; set; }

    }
}

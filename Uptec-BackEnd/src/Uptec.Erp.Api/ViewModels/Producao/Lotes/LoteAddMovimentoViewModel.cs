using System;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.Lotes
{
    public class LoteAddMovimentoViewModel
    {
        public Guid LoteId { get; set; }
        public int LoteSequencia { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public string NotaFiscal { get; set; }
        public TipoMovimentoEstoque TipoMovimento { get; set; }
        public string Historico { get; set; }
    }
}

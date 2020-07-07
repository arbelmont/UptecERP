
using System;
using System.ComponentModel.DataAnnotations;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.Lotes
{
    public class LoteMovimentoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public Guid LoteId { get; set; }
        public int LoteSequencia { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoTotal { get; set; }
        public string NotaFiscal { get; set; }
        public TipoMovimentoEstoque TipoMovimento { get; set; }
        public string Historico { get; set; }
        public LoteViewModel Lote { get; set; }
    }
}

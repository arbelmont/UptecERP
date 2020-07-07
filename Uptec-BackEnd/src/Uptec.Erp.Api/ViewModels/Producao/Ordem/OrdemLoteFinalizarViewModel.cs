using System;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.Ordem
{
    public class OrdemLoteFinalizarViewModel
    {
        public Guid Id { get; set; }
        public int LoteNumero { get; set; }
        public int LoteSequencia { get; set; }
        public decimal Qtde { get; set; }
        public decimal QtdeProduzida { get; set; }
        public DateTime Validade { get; set; }
        public OrdemMotivoExpedicao MotivoExpedicao {get;set;}
        public Guid LoteId { get; set; }
        public Guid OrdemId { get; set; }
    }
}

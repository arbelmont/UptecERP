using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Uptec.Erp.Api.ViewModels.Producao.Lotes;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.Ordem
{
    public class OrdemLoteNfeSaidaViewModel
    {
        public Guid Id { get; set; }
        public int LoteNumero { get; set; }
        public int LoteSequencia { get; set; }
        public decimal Qtde { get; set; }
        public decimal? QtdeProduzida { get; set; }
        public DateTime? Validade { get; set; }
        public OrdemMotivoExpedicao MotivoExpedicao { get; set; }
        public Guid LoteId { get; set; }
        public Guid OrdemId { get; set; }
        public LoteViewModel Lote { get; set; }
        public OrdemNfeSaidaViewModel Ordem { get; set; }
        public string LoteSequenciaString => GetLoteSequencia();

        private string GetLoteSequencia()
        {
            return LoteSequencia > 0 ? $"{LoteNumero}/{LoteSequencia}" : LoteNumero.ToString();
        }
    }
}

using System;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Lotes.Models
{
    public struct LoteDadosSaida
    {
        public Guid Id { get; set; }
        public int LoteNumero { get; set; }
        public int LoteSequencia { get; set; }
        public decimal PrecoSaidaServico { get; set; }
        public decimal PrecoSaidaRemessa { get; set; }
        public string CfopSaidaServico { get; set; }
        public string CfopSaidaRemessa { get; set; }
        public string CodigoPeca { get; set; }
        public string CodigoPecaSaida { get; set; }
        public string DescricaoPeca { get; set; }
        //public decimal PrecoEntrada { get; set; }
        public string Ncm { get; set; }
        public UnidadeMedida UnidadeMedida { get; set; }
        public string LoteSequenciaString => GetLoteSequencia();

        private string GetLoteSequencia()
        {
            return LoteSequencia > 0? $"{LoteNumero}/{LoteSequencia}" : LoteNumero.ToString();
        }
    }
}

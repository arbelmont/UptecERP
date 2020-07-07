
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Uptec.Erp.Api.ViewModels.Producao.Pecas;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.Lotes
{
    public class LoteViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public int LoteNumero { get; set; }
        public Guid PecaId { get; set; }
        public TipoPeca TipoPeca { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Saldo { get; set; }
        public decimal PrecoEntrada { get; set; }
        public string CfopEntrada { get; set; }
        public string NotaFiscal { get;  set; }
        public string NotaFiscalCobertura { get; set; }
        public DateTime DataFabricacao { get; set; }
        public DateTime DataValidade { get; set; }
        public string Localizacao { get; set; }
        public decimal QtdeConcilia { get; set; }
        public LoteStatus Status { get; set; }
        public int Sequencia { get; set; }
        public PecaViewModel Peca { get;  set; }
        public List<LoteMovimentoViewModel> Movimentos { get; set; }
        public string LoteSequenciaString => GetLoteSequencia();

        private string GetLoteSequencia()
        {
            return Sequencia > 0? $"{LoteNumero}/{Sequencia}" : LoteNumero.ToString();
        }

    }
}

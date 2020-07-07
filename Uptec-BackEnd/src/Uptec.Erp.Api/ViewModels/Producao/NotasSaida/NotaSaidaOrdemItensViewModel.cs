using System;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.NotasSaida
{
    public class NotaSaidaOrdemItensViewModel
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Cfop { get; set; }
        public UnidadeMedida Unidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoTotal { get; set; }
        public decimal Quantidade { get; set; }
        public int OrdemNumero { get; set; }
        public Guid OrdemLoteId { get; set; }
        public int LoteNumero { get; set; }
        public int LoteSequencia { get; set; }
    }
}

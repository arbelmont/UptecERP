using System;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.NotasSaida
{
    public class NotaSaidaItensViewModel
    {
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Cfop { get; set; }
        public string Ncm { get; set; }
        public UnidadeMedida Unidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal AliquotaBaseCalculo { get; set; }
        public decimal AliquotaIcms { get; set; }
        public decimal AliquotaIpi { get; set; }
        public decimal AliquotaIva { get; set; }
        public decimal AliquotaPis { get; set; }
        public decimal AliquotaCofins { get; set; }
        public decimal ValorBaseCalculo { get; set; }
        public decimal ValorIcms { get; set; }
        public decimal ValorPis { get; set; }
        public decimal ValorCofins { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal Quantidade { get; set; }
        public int? OrdemNumero { get; set; }
        public int LoteNumero { get; set; }
        public int LoteSequencia { get; set; }
        public Guid NotaSaidaId { get; set; }
        public TipoNotaSaidaItem TipoItem { get; set; }
    }
}

using System;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.Lotes
{
    public class LoteDadosSaidaViewModel
    {
        public Guid Id { get; set; }
        public decimal PrecoSaidaServico { get; set; }
        public decimal PrecoSaidaRemessa { get; set; }
        public string CfopSaidaServico { get; set; }
        public string CfopSaidaRemessa { get; set; }
        public UnidadeMedida UnidadeMedida { get; set; }
        public string CodigoPeca { get; set; }
        public string CodigoPecaSaida { get; set; }
        public string DescricaoPeca { get; set; }
    }
}


using System;
using System.Collections.Generic;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.Ordem
{
    public class OrdemViewModel
    {
        public Guid Id { get; set; }
        public int OrdemNumero { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime? DataProducao { get; set; }
        public decimal QtdeTotal { get; set; }
        public decimal? QtdeTotalProduzida { get; set; }
        public StatusOrdem Status { get; set; }
        public Guid? ClienteId { get; set; }
        public Guid? FornecedorId { get; set; }
        public string CodigoPeca { get; set; }
        public string DescricaoPeca { get; set; }
        public List<OrdemLoteViewModel> OrdemLotes { get; set; }
    }
}

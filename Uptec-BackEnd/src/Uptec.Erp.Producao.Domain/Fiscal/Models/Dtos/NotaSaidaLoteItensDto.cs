using System;

namespace Uptec.Erp.Producao.Domain.Fiscal.Models.Dtos
{
    public class NotaSaidaLoteItensDto
    {
        public Guid LoteId { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public string Cfop { get; set;}
    }
}

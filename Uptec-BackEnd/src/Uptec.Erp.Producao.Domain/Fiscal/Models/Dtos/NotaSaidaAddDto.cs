using System;
using System.Collections.Generic;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Fiscal.Models.Dtos
{
    public class NotaSaidaAddDto
    {
        public Guid DestinatarioId { get; set; }
        public Guid EnderecoId { get; set; }
        public Guid TransportadoraId { get; set; }
        public TipoDestinatario TipoDestinatario { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal ValorSeguro { get; set; }
        public decimal ValorOutrasDespesas { get; set; }
        public decimal ValorDesconto { get; set; }
        public string OutrasInformacoes { get; set; }
        public TipoNotaSaida TipoNota { get; set; }
        public List<NotaSaidaOrdemItensDto> OrdemItens { get; set; }
        public List<NotaSaidaLoteItensDto> LoteItens { get; set; }
    }
}

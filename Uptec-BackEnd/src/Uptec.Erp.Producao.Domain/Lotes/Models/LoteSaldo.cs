
using System;

namespace Uptec.Erp.Producao.Domain.Lotes.Models
{
    public class LoteSaldo
    {
        public Guid Id { get; private set; }
        public string Cliente { get; private set; }
        public string Codigo { get; private set; }
        public string Produto { get; private set; }
        public decimal? Entrada { get; private set; }
        public decimal? Saldo { get; private set; }
    }
}


using System;

namespace Uptec.Erp.Producao.Domain.Ordens.Models
{
    public class LinhaProducao
    {
        public Guid Id { get; private set; }
        public string Cliente { get; private set; }
        public string Materia { get; private set; }
        public string Produto { get; private set; }
        public decimal? Entrada { get; private set; }
        public decimal? Saldo { get; private set; }
        public decimal? Producao { get; private set; }
        public decimal? Expedicao { get; private set; }
    }
}

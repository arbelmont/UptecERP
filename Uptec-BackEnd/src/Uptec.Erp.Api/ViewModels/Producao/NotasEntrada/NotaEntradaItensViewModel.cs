using System;
using System.ComponentModel.DataAnnotations;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.NotasEntrada
{
    public class NotaEntradaItensViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Codigo { get; set; }
        public string CodigoCliente { get; set; }
        public string Descricao { get; set; }
        public string Cfop { get; set; }
        public UnidadeMedida Unidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoTotal { get; set; }
        public decimal Quantidade { get; set; }
        public DateTime? DataFabricacao { get; set; }
        public DateTime? DataValidade { get; set; }
        public string Localizacao { get; set; }
        public decimal? QtdeConcilia { get; set; }
        public string NumeroNotaCobertura { get; set; }
        public StatusNfEntradaItem Status { get; set; }
        public int Lote { get; set; }

        public Guid NotaEntradaId { get; set; }
        //public NotaEntradaViewModel NotaEntrada { get; set; }
    }
}

using System;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.NotasEntrada
{
    public class NotaEntradaAddViewModel
    {
        public string NumeroNota { get; set;}
        public string NumeroNotaCobertura { get; set;}
        public int? NumeroLote { get; set;}
        public DateTime Data { get; set;}
        public decimal Valor { get; set;}
        public string Cfop { get; set;}
        public string CnpjCliente { get; set;}
        public string CnpjFornecedor { get; set;}
        public TipoEstoque TipoEstoque { get; set;}
        public string Xml { get; set; }
    }
}

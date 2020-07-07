using System;
using System.Collections.Generic;
using Uptec.Erp.Api.ViewModels.Producao.Clientes;
using Uptec.Erp.Api.ViewModels.Producao.Fornecedores;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.NotasSaida
{
    public class NotaSaidaViewModel
    {
        public Guid Id { get; set; }
        public string NumeroNota { get; set; }
        public DateTime Data { get; set; }
        public decimal ValorTotalProdutos { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal ValorSeguro { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorOutrasDespesas { get; set; }
        public decimal ValorBaseCalculo { get; set; }
        public decimal ValorIcms { get; set; }
        public decimal ValorIpi { get; set; }
        public decimal ValorPis { get; set; }
        public decimal ValorCofins { get; set; }
        public decimal ValorTotalNota { get; set; }
        public TipoDestinatario TipoDestinatario { get; set; }
        public Guid ArquivoId { get; set; }
        public Guid? ClienteId { get; set; }
        public Guid? FornecedorId { get; set; }
        public Guid EnderecoId { get; set; }
        public StatusNfSaida Status { get; set; }
        public string OutrasInformacoes { get; set; }
        public string ErroApi { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public FornecedorViewModel Fornecedor { get; set; }
        public string EnderecoCompleto { get; set; }
        public virtual ICollection<NotaSaidaItensViewModel> Itens { get; set; }
    }
}

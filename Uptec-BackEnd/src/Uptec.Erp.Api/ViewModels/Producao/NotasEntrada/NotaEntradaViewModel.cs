using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Api.ViewModels.Producao.NotasEntrada
{
    public class NotaEntradaViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string NumeroNota { get; set; }
        public DateTime Data { get; set; }
        public DateTime? DataConciliacao { get; set; }
        public decimal Valor { get; set; }
        public string Cfop { get; set; }
        public string CnpjEmissor { get; set; }
        public string NomeEmissor { get; set; }
        public string EmailEmissor { get; set; }
        public TipoEmissor TipoEmissor { get; set; }
        public TipoEstoque TipoEstoque { get; set; }
        public StatusNfEntrada Status { get; set; }
        public Guid ArquivoId { get; set; }
        public List<NotaEntradaItensViewModel> Itens { get; set; }
        public List<string> Inconsistencias { get; set; }
        public int QtdeNotasAcobrir { get; set; }

        public NotaEntradaViewModel()
        {
            Inconsistencias = new List<string>();
            Itens = new List<NotaEntradaItensViewModel>();
        }
    }
}

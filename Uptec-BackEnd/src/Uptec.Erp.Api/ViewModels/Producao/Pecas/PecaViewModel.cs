using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Uptec.Erp.Producao.Domain.Pecas.Models;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Api.ViewModels.Producao.Clientes;

namespace Uptec.Erp.Api.ViewModels.Producao.Pecas
{
    public class PecaViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid ClienteId { get; set; }

        [Required]
        [MaxLength(Peca.CodigoMaxLenght + 4, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Codigo { get; set; }

        [MaxLength(Peca.CodigoSaidaMaxLenght, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string CodigoSaida { get; set; }

        [Required]
        [MaxLength(Peca.DescricaoMaxLenght, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Descricao { get; set; }

        [Required]
        public UnidadeMedida Unidade { get; set; }

        [Required]
        public TipoPeca Tipo { get; set; }

        [Required]
        public decimal Preco { get; set; }

        [Required]
        public decimal PrecoSaida { get; set; }

        [Required]
        [MaxLength(Peca.NcmMaxLenght, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Ncm { get; set; }

        [MaxLength(Peca.RevisaoMaxLenght, ErrorMessage = "{0} deve conter no máximo {1} caracteres.")]
        public string Revisao { get; set; }

        public List<PecaComponenteViewModel> Componentes { get; set; }

        public List<PecaFornecedorCodigoViewModel> CodigosFornecedor { get; set; }

        public ClienteViewModel Cliente {get; set;}

        public PecaViewModel()
        {
            Componentes = new List<PecaComponenteViewModel>();
            CodigosFornecedor = new List<PecaFornecedorCodigoViewModel>();
        }
    }
}

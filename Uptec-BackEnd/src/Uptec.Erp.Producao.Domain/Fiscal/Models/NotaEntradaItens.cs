using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using System;
using Uptec.Erp.Producao.Domain.Fiscal.Validations;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Fiscal.Models
{
    public class NotaEntradaItens : Entity<NotaEntradaItens>
    {
        public string Codigo { get; private set; }
        public string CodigoCliente { get; private set; }
        public string Descricao { get; private set; }
        public string Cfop { get; private set; }
        public UnidadeMedida Unidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public decimal PrecoTotal { get; private set; }
        public decimal Quantidade { get; private set; }
        public int? Lote { get; private set; }
        public DateTime? DataFabricacao { get; private set; }
        public DateTime? DataValidade { get; private set; }
        public string Localizacao { get; private set; }
        public decimal? QtdeConcilia {get; private set; }
        public string NumeroNotaCobertura { get; private set; }
        public StatusNfEntradaItem Status { get; private set; }

        public Guid NotaEntradaId { get; private set; }
        public virtual NotaEntrada NotaEntrada { get; private set; }

        protected NotaEntradaItens() {}

        public NotaEntradaItens(Guid id, string codigo, string descricao, string cfop, UnidadeMedida unidadeMedida, 
                                decimal precoUnitario, decimal quantidade, Guid notaEntradaId)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
            Cfop = cfop;
            Unidade = unidadeMedida;
            PrecoUnitario = precoUnitario;
            Quantidade = quantidade;
            PrecoTotal = (PrecoUnitario * Quantidade);
            NotaEntradaId = notaEntradaId;
            Status = StatusNfEntradaItem.Conciliar;
        }

        public override bool IsValid()
        {
            Validation = new Validation(new NotaEntradaItensValidation().Validate(this), new NotaEntradaItensSystemValidation().Validate(this));

            return Validation.IsValid();
        }

        public void SetCodigoCliente(string codigo)
        {
            CodigoCliente = codigo;
        }

        public void SetLote(int lote)
        {
            Lote = lote;
        }

        public void SetNotaCobertura(string numeroNota)
        {
            NumeroNotaCobertura = numeroNota;
        }

        public void SetDataFabricacao(DateTime data)
        {
            DataFabricacao = data;
        }

        public void SetDataValidade(DateTime data)
        {
            DataValidade = data;
        }

        public void SetLocalizacao(string local)
        {
            Localizacao = local;
        }

        public void SetQtdeConcilia(decimal qtde)
        {
            QtdeConcilia = qtde;
        }

        public void SetStatus(StatusNfEntradaItem status)
        {
            Status = status;
        }

        public const byte CodigoMaxLenght = 20;
        public const byte DescricaoMaxLenght = 200;
        public const byte CfopMaxLenght = 5;
        public const byte NumeroNotaMaxLenght = 50;
        public const byte LocalizacaoMaxLenght = 30;
    }
}

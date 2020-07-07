using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using System;
using Uptec.Erp.Producao.Domain.Lotes.Validations;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Lotes.Models
{
    public class LoteMovimento : Entity<LoteMovimento>
    {
        public DateTime Data { get; private set; }
        public Guid LoteId { get; private set; }
        public int LoteSequencia { get; private set; }
        public decimal Quantidade { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public decimal PrecoTotal { get; private set; }
        public string NotaFiscal { get; private set; }
        public TipoMovimentoEstoque TipoMovimento { get; private set; }
        public string Historico { get; private set; }
        public virtual Lote Lote { get; private set; }

        protected LoteMovimento()
        {
        }

        public LoteMovimento(Guid id, DateTime data, Guid loteId, int loteSequencia, decimal quantidade, decimal precoUnitario, string notaFiscal, TipoMovimentoEstoque tipoMovimento, string historico)
        {
            Id = id;
            Data = data;
            LoteId = loteId;
            LoteSequencia = loteSequencia;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            PrecoTotal = (PrecoUnitario * Quantidade);
            NotaFiscal = notaFiscal;
            TipoMovimento = tipoMovimento;
            Historico = historico;
        }

        public override bool IsValid()
        {
            Validation = new Validation(new LoteMovimentoValidation().Validate(this), new LoteMovimentoSystemValidation().Validate(this));

            return Validation.IsValid();
        }

        public void SetLoteSequencia(int sequencia)
        {
            LoteSequencia = sequencia;
        }

        public const byte HistoricoMaxLenght = 200;
        public const byte NotaFiscalMaxLenght = 50;
    }
}

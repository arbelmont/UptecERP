using System;
using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Producao.Domain.Ordens.Validations;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Ordens.Models
{
    public class OrdemLote : Entity<Ordem>
    {
        
        public int LoteNumero { get; private set; }
        public int LoteSequencia { get; private set; }
        public decimal Qtde { get; private set; }
        public decimal? QtdeProduzida { get; private set; }
        public DateTime? Validade { get; private set; }
        public OrdemMotivoExpedicao MotivoExpedicao { get; private set; }
        public string NotaFiscalSaida { get; private set; }
        public Guid LoteId { get; private set; }
        public Guid OrdemId { get; private set; }
        public virtual Lote Lote { get; private set; }
        public virtual Ordem Ordem { get; private set; }

        protected OrdemLote()
        {
        }

        public OrdemLote(Guid id, int loteNumero, int loteSequencia, decimal quantidade, Guid loteId)
        {
            Id = id;
            LoteNumero = loteNumero;
            LoteSequencia = loteSequencia;
            Qtde = quantidade;
            LoteId = loteId;
            MotivoExpedicao = OrdemMotivoExpedicao.Produzindo;
        }

        public void SetQtdeProduzida(decimal quantidade)
        {
            QtdeProduzida = quantidade;
        }

        public void SetValidade(DateTime validade)
        {
            Validade = validade;
        }

        public void SetOrdemId(Guid ordemId)
        {
            OrdemId = ordemId;
        }

        public void SetMotivoExpedicao(OrdemMotivoExpedicao motivo)
        {
            MotivoExpedicao = motivo;
        }

        public void SetNotaFiscalSaida(string numeroNota)
        {
            NotaFiscalSaida = numeroNota;
        }

        public override bool IsValid()
        {
            Validation = new Validation(new OrdemLoteValidation().Validate(this), new OrdemLoteSystemValidation().Validate(this));

            return Validation.IsValid();
        }

        public string GetLoteSequenciaString()
        {
            return LoteSequencia > 0? $"{LoteNumero}/{LoteSequencia}" : LoteNumero.ToString();
        }

        public const byte NotaFiscalMaxLenght = 50;
    }
}
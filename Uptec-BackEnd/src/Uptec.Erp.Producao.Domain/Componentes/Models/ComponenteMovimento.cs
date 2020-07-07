using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using System;
using Uptec.Erp.Producao.Domain.Componentes.Validations;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Componentes.Models
{
    public class ComponenteMovimento : Entity<ComponenteMovimento>
    {
        public Guid ComponenteId { get; private set; }
        public decimal Quantidade { get; private set; }
        public DateTime Data { get; private set; }
        public TipoMovimentoEstoque TipoMovimento { get; private set; }
        public decimal PrecoUnitario { get; private set; }
        public decimal PrecoTotal { get; private set; }
        public string NotaFiscal { get; private set; } //vira objeto quando tivermos a entidade nota
        public decimal Saldo { get; private set; }
        public string Historico { get; private set; }
        public virtual Componente Componente { get; private set; }

        public ComponenteMovimento(Guid id, 
                                   Guid componenteId, 
                                   decimal quantidade,
                                   TipoMovimentoEstoque tipoMovimento,
                                   decimal precoUnitario,
                                   string notaFiscal,
                                   string historico)
        {
            Id = id;
            ComponenteId = componenteId;
            Quantidade = quantidade;
            Data = DateTime.Now;
            TipoMovimento = tipoMovimento;
            PrecoUnitario = precoUnitario;
            NotaFiscal = notaFiscal;
            Historico = historico;
            PrecoTotal = (Quantidade * PrecoUnitario);
        }

        protected ComponenteMovimento() { }

        public void SetSaldo(decimal saldo)
        {
            Saldo = saldo;
        }

        public override bool IsValid()
        {
            Validation = new Validation(new ComponenteMovimentoValidation().Validate(this), new ComponenteMovimentoSystemValidation().Validate(this));

            return Validation.IsValid();
        }


        public const byte HistoricoMaxLenght = 200;
        public const byte NotaFiscalMaxLenght = 50;
    }
}

using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using System;
using Uptec.Erp.Producao.Domain.Fiscal.Validations;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Enums.NFe;

namespace Uptec.Erp.Producao.Domain.Fiscal.Models
{
    public class NotaSaidaItens : Entity<NotaSaidaItens>
    {
        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
        public string Cfop { get; private set; }
        public string Ncm { get; private set; }
        public UnidadeMedida Unidade { get; private set; }
        public decimal ValorUnitario { get; private set; }
        public decimal AliquotaBaseCalculo { get; private set; }
        public decimal AliquotaIcms { get; private set; }
        public decimal AliquotaIpi { get; set; }
        public decimal AliquotaIva { get; private set; }
        public decimal AliquotaPis { get; private set; }
        public decimal AliquotaCofins { get; private set; }
        public IcmsSituacaoTributariaNfe IcmsSituacaoTributaria { get; private set; }
        public PisSituacaoTributariaNfe PisSituacaoTributaria { get; private set; }
        public CofinsSituacaoTributariaNfe CofinsSituacaoTributaria { get; private set; }
        public decimal ValorBaseCalculo { get; private set; }
        public decimal ValorIcms { get; private set; }
        public decimal ValorIpi { get; private set; }
        public decimal ValorPis { get; private set; }
        public decimal ValorCofins { get; private set; }
        public decimal ValorTotal { get; private set; }
        public decimal Quantidade { get; private set; }
        public int? OrdemNumero { get; private set; }
        public int LoteNumero { get; private set; }
        public int LoteSequencia { get; private set; }
        public Guid NotaSaidaId { get; private set; }
        public TipoNotaSaidaItem TipoItem { get; private set; }
        public virtual NotaSaida NotaSaida { get; private set; }

        protected NotaSaidaItens() {}

        public NotaSaidaItens(Guid id, string codigo, string descricao, string cfop, string ncm, UnidadeMedida unidadeMedida, 
                                decimal precoUnitario, decimal quantidade, int ordemNumero, int loteNumero, int loteSequencia, 
                                Guid notaSaidaId)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
            Cfop = cfop;
            Ncm = ncm;
            Unidade = unidadeMedida;
            ValorUnitario = precoUnitario;
            Quantidade = quantidade;
            ValorTotal = (ValorUnitario * Quantidade);
            OrdemNumero = ordemNumero;
            LoteNumero = loteNumero;
            LoteSequencia = loteSequencia;
            NotaSaidaId = notaSaidaId;
        }

        public override bool IsValid()
        {
            Validation = new Validation(new NotaSaidaItensValidation().Validate(this), new NotaSaidaItensSystemValidation().Validate(this));

            return Validation.IsValid();
        }

        public void SetSituacaoTributaria(IcmsSituacaoTributariaNfe ist, PisSituacaoTributariaNfe pst, CofinsSituacaoTributariaNfe cst)
        {
            IcmsSituacaoTributaria = ist;
            PisSituacaoTributaria = pst;
            CofinsSituacaoTributaria = cst;
        }

        public void CalcularImpostos(decimal aliquotaBaseCalculo, decimal aliquotaIcms, decimal aliquotaPis, 
                                     decimal aliquotaCofins, decimal aliquotaIpi)
        {
            AliquotaBaseCalculo = aliquotaBaseCalculo;
            AliquotaIcms = aliquotaIcms;
            AliquotaPis = aliquotaPis;
            AliquotaCofins = aliquotaCofins;
            AliquotaIpi = aliquotaIpi;

            ValorBaseCalculo = ValorTotal * (AliquotaBaseCalculo / 100);
            
            ValorIcms = ValorBaseCalculo * (AliquotaIcms / 100);
            ValorPis = ValorBaseCalculo * (AliquotaPis / 100);
            ValorCofins = ValorBaseCalculo * (AliquotaCofins / 100);
            ValorIpi = ValorBaseCalculo * (AliquotaIpi / 100);
        }

        public void SetTipoItem(TipoNotaSaidaItem tipo)
        {
            TipoItem = tipo;
        }

        public const byte CodigoMaxLenght = 20;
        public const byte DescricaoMaxLenght = 200;
        public const byte CfopMaxLenght = 5;
        public const byte NcmMaxLenght = 12;
    }
}

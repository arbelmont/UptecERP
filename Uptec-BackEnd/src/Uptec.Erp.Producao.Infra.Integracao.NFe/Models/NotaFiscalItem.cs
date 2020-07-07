using Uptec.Erp.Shared.Domain.Enums.NFe;

namespace Uptec.Erp.Producao.Infra.Integracao.NFe.Models
{
    public class NotaFiscalItem
    {
        public short numero_item { get; set; }
        public string codigo_produto { get; set; }
        public string descricao { get; set; }
        public string cfop { get; set; }
        public string unidade_comercial { get; set; }
        public decimal quantidade_comercial { get; set; }
        public decimal valor_unitario_comercial { get; set; }
        public decimal valor_unitario_tributavel { get; set; }
        public string unidade_tributavel { get; set; }
        public string codigo_ncm { get; set; }
        public decimal quantidade_tributavel { get; set; }
        public decimal valor_bruto { get; set; }
        public IcmsSituacaoTributariaNfe icms_situacao_tributaria { get; set; }
        public IcmsOrigemNfe icms_origem { get; set; }
        public PisSituacaoTributariaNfe pis_situacao_tributaria { get; set; }
        public CofinsSituacaoTributariaNfe cofins_situacao_tributaria { get; set; }

        public short ipi_codigo_enquadramento_legal { get; set; }
        public short ipi_situacao_tributaria { get; set; }

        // public SimNao inclui_no_total { get; set; }
        public decimal icms_aliquota { get; set; }
        public short icms_modalidade_base_calculo { get; set; }
        public decimal icms_base_calculo { get; set; }
        public decimal icms_valor { get; set; }
        public decimal ipi_valor { get; set; }

        public decimal pis_aliquota_porcentual { get; set; }
        public decimal pis_base_calculo { get; set; }
        public decimal pis_valor { get; set; }

        public decimal cofins_aliquota_porcentual { get; set; }
        public decimal cofins_valor { get; set; }
        public decimal cofins_base_calculo { get; set; }
    }
}

using System.Collections.Generic;
using Uptec.Erp.Shared.Domain.Enums.NFe;

namespace Uptec.Erp.Producao.Infra.Integracao.NFe.Models
{
    public class NotaFiscal
    {
        public int numero { get; set; }
        public int serie { get; set; }
        public string natureza_operacao { get; set; }
        public string data_emissao { get; set; }
        public TipoDocumentoNfe tipo_documento { get; set; }
        public LocalDestinoNfe local_destino { get; set; }
        public FinalidadeEmissaoNfe finalidade_emissao { get; set; }
        public ConsumidorFinalNfe consumidor_final { get; set; }
        public PresencaCompradorNfe presenca_comprador { get; set; }
        public ModalidadeFreteNfe modalidade_frete { get; set; }
        public string informacoes_adicionais_contribuinte { get; set; }

        #region Tributos
        public decimal valor_frete { get; set; }
        public decimal valor_seguro { get; set; }
        public decimal valor_total { get; set; }
        public decimal valor_produtos { get; set; }
        public decimal valor_desconto { get; set; }
        public decimal icms_base_calculo { get; set; }
        public decimal icms_valor_total { get; set; }
        public decimal valor_ipi { get; set; }
        public decimal valor_pis { get; set; }
        public decimal valor_cofins { get; set; }
        public decimal valor_outras_despesas { get; set; }
        #endregion

        #region Emitente
        public string nome_emitente { get; set; }
        public string cnpj_emitente { get; set; }
        public string inscricao_estadual_emitente { get; set; }
        public string logradouro_emitente { get; set; }
        public string numero_emitente { get; set; }
        public string bairro_emitente { get; set; }
        public string municipio_emitente { get; set; }
        public string uf_emitente { get; set; }
        public string cep_emitente { get; set; }
        public RegimeTributarioEmitenteNfe regime_tributario_emitente { get; set; }
        #endregion

        #region Destinatário

        public string nome_destinatario { get; set; }
        public string cnpj_destinatario { get; set; }
        public string inscricao_estadual_destinatario { get; set; }
        public string logradouro_destinatario { get; set; }
        public string numero_destinatario { get; set; }
        public string bairro_destinatario { get; set; }
        public string municipio_destinatario { get; set; }
        public string uf_destinatario { get; set; }
        public string pais_destinatario { get; set; }
        public string cep_destinatario { get; set; }
        public IndicadorInscricaoEstadualDestinatarioNfe indicador_inscricao_estadual_destinatario { get; set; }
        #endregion

        #region Transportadora
        public string cnpj_transportador { get; set; }
        public string nome_transportador { get; set; }
        public string inscricao_estadual_transportador { get; set; }
        public string endereco_transportador { get; set; }
        public string municipio_transportador { get; set; }
        public string uf_transportador { get; set; }
        #endregion

        public ICollection<NotaFiscalItem> items { get; set; }
    }
}

using System;
using Uptec.Erp.Shared.Domain.Enums.NFe;
using Uptec.Erp.Shared.Domain.Enums.NFe.Processamento;

namespace Uptec.Erp.Producao.Domain.Fiscal.Models.Dtos.Integracao.Receita
{
    public class CabecalhoNfeDto
    {
        public string Nome_emitente { get; set; }
        public string Documento_emitente { get; set; }
        public string Cnpj_destinatario { get; set; }
        public string Chave_nfe { get; set; }
        public string Valor_total { get; set; }
        public DateTime? Data_emissao { get; set; }
        public SituacaoNfe? Situacao { get; set; }
        public ManifestacaoStatus? Manifestacao_destinatario { get; set; }
        public string Justificativa { get; private set; }
        public bool Nfe_completa { get; set; }
        public TipoNfe? Tipo_nfe { get; set; }
        public int Versao { get; set; }
        public string Digest_value { get; set; }
        public int? Numero_carta_correcao { get; set; }
        public string Carta_correcao { get; set; }
        public DateTime? Data_carta_correcao { get; set; }
        public DateTime? Data_cancelamento { get; set; }
        public string Justificativa_cancelamento { get; set; }
    }
}

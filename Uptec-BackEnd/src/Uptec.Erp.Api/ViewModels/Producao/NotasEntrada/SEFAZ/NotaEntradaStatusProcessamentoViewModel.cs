namespace Uptec.Erp.Api.ViewModels.Producao.NotasEntrada.SEFAZ
{
    public class NotaEntradaStatusProcessamentoViewModel
    {
        public string Nome_emitente { get; set; }
        public string Documento_emitente { get; set; }
        public string Chave_nfe { get; set; }
        public decimal Valor_total { get; set; }
        public string Data_emissao { get; set; }
        public string Situacao { get; set; }
        public string Manifestacao_destinatario { get; set; }
        public bool Nfe_completa { get; set; }
        public string Tipo_nfe { get; set; }
        public string Versao { get; set; }
        public string Digest_value { get; set; }
        public string Cnpj_destinatario { get; set; }
    }
}

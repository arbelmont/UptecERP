namespace Uptec.Erp.Api.ViewModels.Producao.NotasSaida
{
    public class NotaSaidaStatusProcessamentoViewModel
    {
        public string Cnpj_emitente { get; set; }
        public string Ref { get; set; }
        public string Status { get; set; }
        public string Status_sefaz { get; set; }
        public string Mensagem_sefaz { get; set; }
        public string Chave_nfe { get; set; }
        public string Numero { get; set; }
        public string Serie { get; set; }
        public string Caminho_xml_nota_fiscal { get; set; }
        public string Caminho_danfe { get; set; }
    }
}

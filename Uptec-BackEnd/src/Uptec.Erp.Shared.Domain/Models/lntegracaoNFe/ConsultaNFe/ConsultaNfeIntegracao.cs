namespace Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.ConsultaNFe
{
    public class ConsultaNfeIntegracao
    {
        public string Cnpj_emitente { get; set; }
        public string Ref { get; set; }
        public string Status { get; set; }
        public string Status_Sefaz { get; set; }
        public string Mensagem_Sefaz { get; set; }
        public string Chave_Nfe { get; set; }
        public string Numer { get; set; }
        public string Serie { get; set; }
        public string Caminho_Xml_Nota_Fiscal { get; set; }
        public string Caminho_Danfe { get; set; }
    }
}

namespace Uptec.Erp.Shared.Domain.Enums.NFe
{
    public enum IcmsOrigemNfe : byte
    {
        Nacional = 0, 
        EstrangeiraImportacaoDireta = 1, 
        EstrangeiraAdquiridaNoMercadoInterno = 2, 
        NacionalComMaisDe40PorcentoDeConteudoEstrangeiro = 3, 
        NacionalProduzidaAtravesDeProcessosProdutivosBasicos = 4, 
        NacionalComMenosDe40PorcentoDeConteudoEstrangeiro = 5, 
        EstrangeiraImportacaoDiretaSemProdutoNacionalSimilar = 6,
        EstrangeiraAdquiridaNoMercadoInternoSemProdutoNacionalSimilar = 7  
    }
}
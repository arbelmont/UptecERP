namespace Uptec.Erp.Shared.Domain.Enums.NFe
{
    public enum IcmsSituacaoTributariaNfe : short
    {
        TributadaIntegralmente = 00,
        TributadaComCobrancaDoIcmsPorSubstituicaoTributaria = 10,
        TributadaComReducaoDeBaseDeCalculo = 20,
        IsentaOuNaoTributadaEComCobrancaDoIcmsPorSubstituicaoTributaria = 30,
        Isenta = 40,
        NaoTributada = 41,
        Suspencao = 50,
        Diferimento = 51, // (a exigência do preenchimento das informações do Icms diferido fica a critérioDe cada UF)
        CobradoAnteriormentePorSubstituicaoTributaria = 60,
        TributadaComReducaoDeBaseDeCalculoEComCobrancaDoIcmsPorSubstituicaoTributaria = 70,
        OutrasRegimeNormal = 90,
        TributadaPeloSimplesNacionalComPermissaoDeCredito = 101,
        TributadaPeloSimplesNacionalSemPermissaoDeCredito = 102,
        IsencaoDoIcmsNoSimplesNacionalParaFaixaDeReceitaBruta = 103,
        TributadaPeloSimplesNacionalComPermissaoDeCreditoEComCobrancadoIcmsPorSubstituicaoTributaria = 201,
        TributadaPeloSimplesNacionalsemPermissaoDeCreditoEComCobrancadoIcmsPorSubstituicaoTributaria = 202,
        IsencaoDoIcmsNosSimplesNacionalParaFaixaDeReceitaBrutaEComCobrancaDoIcmsPorSubstituicaoTributaria = 203,
        Imune = 300,
        NaoTributadaPeloSimplesNacional = 400,
        IcmsCobradoAnteriormentePorSubstituicaoTributariaSubstituidoOuPorAntecipacao = 500,
        OutrasRegimeSimplesNacional = 900,
    }
}
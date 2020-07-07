namespace Uptec.Erp.Shared.Domain.Enums.NFe
{
    public enum PisSituacaoTributariaNfe : byte
    {
        OperacaoTributavelBaseDeCalculoIgualValorOperacaoAliquotaNormalMenoscumulativoDivididoNaocumulativo = 01,
        OperacaoTributavelBaseDeCalculoIgualValorOperacaoAliquotadiferenciada = 02,
        OperacaoTributavelBaseDeCalculoIgualQuantidadeVendidaVezesAliquotaPorUnidadeDeProduto = 03,
        OperacaoTributavelTributacaoMonofasicaAliquotaZero = 04,
        OperacaoTributavelSubstituicaoTributaria = 05,
        OperacaoTributavelAliquotazero = 06,
        OperacaoIsentaDaContribuicao = 07,
        OperacaoSemIncidenciaDaContribuicao = 08,
        OperacaoComSuspencaoDaContribuicao = 09,
        OutrasOperacoesDeSaida = 49,
        OperacaoComDireitoACreditoVinculadaExclusivamenteAreceitaTributadaNoMercadoInterno = 50,
        OperacaoComDireitoACreditoVinculadaExclusivamenteAreceitaNaoTributadaNoMercadoInterno = 51,
        OperacaoComDireitoACreditoVinculadaExclusivamenteAreceitaDeExportacao = 52,
        OperacaoComDireitoACreditoVinculadaAReceitasTributadasENaoTributadasNoMercadoInterno = 53,
        OperacaoComDireitoACreditoVinculadaAReceitasTributadasNoMercadoInternoEDeExportacao = 54,
        OperacaoComDireitoACreditoVinculadaAReceitasNaoTributadasNoMercadoInternoEDeExportacao = 55,
        OperacaoComDireitoACreditoVinculadaAReceitasTributadasENaoTributadasNoMercadoInternoEDeExportacao = 56,
        CreditoPresumidoOperacaoDeAquisicaoVinculadaExclusivamenteAreceitaTributadaNoMercadoInterno = 60,
        CreditoPresumidoOperacaoDeAquisicaoVinculadaExclusivamenteAreceitaNaoTributadaNoMercadoInterno = 61,
        CreditoPresumidoOperacaoDeAquisicaoVinculadaExclusivamenteAreceitaDeExportacao = 62,
        CreditoPresumidoOperacaoDeAquisicaoVinculadaAReceitasTributadasENaoTributadasNoMercadoInterno = 63,
        CreditoPresumidoOperacaoDeAquisicaoVinculadaAReceitasTributadasNoMercadoInternoEDeExportacao = 64,
        CreditoPresumidoOperacaoDeAquisicaoVinculadaAReceitasNaoTributadasNoMercadoInternoEDeExportacao = 65,
        CreditoPresumidoOperacaoDeAquisicaoVinculadaAReceitasTributadasENaoTributadasNoMercadoInternoEDeExportacao = 66,
        CreditoPresumidoOutrasOperacoes = 67,
        OperacaoDeAquisicaoSemDireitoACredito = 70,
        OperacaoDeAquisicaoComIsencao = 71,
        OperacaoDeAquisicaoComSuspencao = 72,
        OperacaoDeAquisicaoAliquotaZero = 73,
        OperacaoDeAquisicaoSemIncidenciaDaContribuicao = 74,
        OperacaoDeAquisicaoPorSubstituicaoTributaria = 75,
        OutrasOperacoesDeEntrada = 98,
        OutrasOperacoes = 99
    }
}
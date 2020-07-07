namespace Uptec.Erp.Shared.Domain.Enums.NFe
{
    public enum CofinsSituacaoTributariaNfe : byte
    {
        OperacaoTributavelBaseDeCalculoIgualValorDaOperacaoAliquotaNormalMenosCumulativoDivididoNaoCumulativo = 01,
        OperacaoTributavelBaseDeCalculoIgualValorDaOperacaoAliquotaDiferenciada = 02,
        OperacaoTributavelBaseDeCalculoIgualQuantidadeVendidaVezesAliquotaPorUnidadedeProduto = 03,
        OperacaoTributavelTributacaoMonofasicaAliquotazero = 04,
        OperacaoTributavelSubstituicaoTributaria = 05,
        OperacaoTributavelAliquotazero = 06,
        OperacaoIsentaContribuicao = 07,
        OperacaoSemIncidenciaDaContribuicao = 08,
        OperacaoComSuspensãoDaContribuicao = 09,
        OutrasOperacoesDeSaida = 49,
        OperacaoComDireitoCreditoVinculadaExclusivamenteAReceitaTributadaMercadoInterno = 50,
        OperacaoComDireitoCreditoVinculadaExclusivamenteAReceitaNaoTributadaMercadoInterno = 51,
        OperacaoComDireitoCreditoVinculadaExclusivamenteAReceitadeExportacao = 52,
        OperacaoComDireitoCreditoVinculadaaReceitasTributadasNaoTributadasMercadoInterno = 53,
        OperacaoComDireitoCreditoVinculadaaReceitasTributadasMercadoInternodeExportacao = 54,
        OperacaoComDireitoCreditoVinculadaaReceitasNaoTributadasMercadoInternodeExportacao = 55,
        OperacaoComDireitoCreditoVinculadaaReceitasTributadasNaoTributadasMercadoInternoExportacao = 56,
        CreditoPresumidoOperacaoDeAquisicaoVinculadaExclusivamenteAReceitaTributadaMercadoInterno = 60,
        CreditoPresumidoOperacaoDeAquisicaoVinculadaExclusivamenteAReceitaNaoTributadaMercadoInterno = 61,
        CreditoPresumidoOperacaoDeAquisicaoVinculadaExclusivamenteAReceitaExportacao = 62,
        CreditoPresumidoOperacaoDeAquisicaoVinculadaReceitasTributadasNaoTributadasMercadoInterno = 63,
        CreditoPresumidoOperacaoDeAquisicaoVinculadaReceitasTributadasMercadoInternoExportacao = 64,
        CreditoPresumidoOperacaoDeAquisicaoVinculadaReceitasNaoTributadasMercadoInternoExportacao = 65,
        CreditoPresumidoOperacaoDeAquisicaoVinculadaReceitasTributadasNaoTributadasMercadoInternoExportacao = 66,
        CreditoPresumidoOutrasOperacoes = 67,
        OperacaoDeAquisicaoSemDireitoACredito = 70,
        OperacaoDeAquisicaoComIsenção = 71,
        OperacaoDeAquisicaoComSuspensão = 72,
        OperacaoDeAquisicaoAliquotazero = 73,
        OperacaoDeAquisicaoSemIncidenciaDaContribuicao = 74,
        OperacaoDeAquisicaoPorSubstituicaoTributaria = 75,
        OutrasOperacoesEntrada = 98,
        OutrasOperacoes = 99
    }
}

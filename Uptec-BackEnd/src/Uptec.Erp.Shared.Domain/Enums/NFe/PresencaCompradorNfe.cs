namespace Uptec.Erp.Shared.Domain.Enums.NFe
{
    public enum PresencaCompradorNfe
    {
        NaoSeAplica = 0, // (por exemplo, para a Nota Fiscal complementar ou de ajuste)
        OperacaoPresencial = 1,
        OperacaoNaoPresencialPelaInternet = 2,
        OperacaoNaoPresencialTeleatendimento = 3,
        NFCeOperacaoComEntregaEmDomicilio = 4,
        OperacaoNaoPresencialOutros = 9
    }
}
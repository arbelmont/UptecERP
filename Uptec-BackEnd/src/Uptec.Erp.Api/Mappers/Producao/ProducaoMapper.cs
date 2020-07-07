using Uptec.Erp.Api.Mappers.Producao.Map;

namespace Uptec.Erp.Api.Mappers
{
    public class ProducaoMapper
    {
        public ProducaoMapper()
        {
            new ArquivoMapper();
            new ClienteMapper();
            new ComponenteMapper();
            new FornecedorMapper();
            new OrdemMapper();
            new PecaMapper();
            new TransportadoraMapper();
            new LoteMapper();
            new NotaEntradaMapper();
            new NotaSaidaMapper();
            new NotaFiscalIntegracaoMapper();
        }
    }
}
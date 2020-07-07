using System.Collections.Generic;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Dtos.Integracao.Receita;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Integracao.Receita;
using Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.ConsultaNFe;
using Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.Erros;

namespace Uptec.Erp.Producao.Domain.Fiscal.Interfaces.Integracao
{
    public interface INotaFiscalIntegracaoManifestacao
    {
        bool TryManifestar(CabecalhoNfe cabecalhoNfe,
                           out ManifestacaoNfeResult manifestacaoNfeResult,
                           out MensagemErro mensagemErro);
        bool TryGetUnmanifestedFromIntegration(out IEnumerable<CabecalhoNfeDto> cabecalhosNfes,
                                               out MensagemErro mensagemErro,
                                               int VersaoMax);

        bool TryGetXmlNfeFromIntegrationByChave(string chaveNfe,
                                                out string xmlNfe,
                                                out MensagemErro mensagemErro);

        bool TryConsultar(string numeroNota, out ConsultaNfeIntegracao consultaNfeIntegracao,
                          out MensagemErro mensagemErro, bool completa = false);

        bool TryObterArquivo(string numeroNota, out byte[] conteudoArquivo, out MensagemErro mensagemErro);
    }
}


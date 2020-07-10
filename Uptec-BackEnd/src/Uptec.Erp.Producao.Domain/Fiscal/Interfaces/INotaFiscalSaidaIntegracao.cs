using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.ConsultaNFe;
using Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.Erros;

namespace Uptec.Erp.Producao.Domain.Fiscal.Interfaces
{
    public interface INotaFiscalSaidaIntegracao
    {
        bool TryEnviar(NotaSaida notaSaida, out MensagemErro mensagemErro);

        bool TryConsultar(string numeroNota, out ConsultaNfeIntegracao consultaNfeIntegracao,
                          out MensagemErro mensagemErro, bool completa = false);

        bool TryObterArquivo(string caminhoArquivo, out byte[] conteudoArquivo, out MensagemErro mensagemErro);

        bool Cancelar(string numeroNota, out MensagemErro mensagemErro);
    }
}

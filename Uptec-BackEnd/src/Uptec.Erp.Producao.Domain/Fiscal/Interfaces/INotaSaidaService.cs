using System;
using Uptec.Erp.Producao.Domain.Fiscal.Events;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Dtos;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Impostos;
using Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.ConsultaNFe;
using Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.Erros;

namespace Uptec.Erp.Producao.Domain.Fiscal.Interfaces
{
    public interface INotaSaidaService : IDisposable
    {
        NotaSaida GetWithUpdateStatusSefaz(Guid id, out bool hasChanges);
        NotaSaidaAddedEvent Add(NotaSaidaAddDto notaSaidaDto);

        void Update(NotaSaida notaSaida);

        void Delete(Guid id);

        AliquotaImpostos GetAliquotaImpostos(string uf);

        bool Cancelar(string numeroNota,  out MensagemErro mensagemErro);

        bool TryEnviar(NotaSaida notaSaida, out MensagemErro mensagemErro);
        NotaSaida Reenviar(Guid notaId, out bool hasChanges);

        bool TryConsultar(string numeroNota, out ConsultaNfeIntegracao consultaNfeIntegracao,
                          out MensagemErro mensagemErro, bool completa = false);

        bool TryObterArquivo(string numeroNota, out byte[] conteudoArquivo, out MensagemErro mensagemErro, TipoArquivo tipoArquivo);

        bool TryChangeStatusProcessamento(ConsultaNfeIntegracao consultaNfeIntegracao);
    }
}
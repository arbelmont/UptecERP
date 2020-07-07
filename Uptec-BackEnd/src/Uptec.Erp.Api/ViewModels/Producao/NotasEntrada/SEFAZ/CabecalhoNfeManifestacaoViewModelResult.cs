using System;
using Uptec.Erp.Shared.Domain.Enums.NFe.Processamento;

namespace Uptec.Erp.Api.ViewModels.Producao.NotasEntrada.SEFAZ
{
    public class CabecalhoNfeManifestacaoViewModelResult
    {
        public Guid Id { get; set; }
        public string ChaveNfe { get; set; }
        public ManifestacaoStatus? ManifestacaoDestinatario { get; set; }
        public string Justificativa { get; private set; }
        public string Notificacao { get; set; }
    }
}

using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Services;
using Definitiva.Shared.Infra.Support.Helpers;
using System;
using System.Collections.Generic;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces.Integracao;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Dtos.Integracao.Receita;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Integracao.Receita;
using Uptec.Erp.Producao.Domain.Fiscal.Validations;
using Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.ConsultaNFe;
using Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.Erros;

namespace Uptec.Erp.Producao.Domain.Fiscal.Services.Integracao.Receita
{
    public class ManifestacaoNfeService : BaseService, IManifestacaoNfeService
    {
        private readonly ICabecalhoNfeRepository _cabecalhoNfeRepository;
        private readonly INotaFiscalIntegracaoManifestacao _notaFiscalIntegracaoManifestacao;

        public ManifestacaoNfeService(IBus bus,
                                      ICabecalhoNfeRepository manifestacaoRepository,
                                      INotaFiscalIntegracaoManifestacao notaFiscalIntegracaoManifestacao) : base(bus)
        {
            _cabecalhoNfeRepository = manifestacaoRepository;
            _notaFiscalIntegracaoManifestacao = notaFiscalIntegracaoManifestacao;
        }

        public void Add(CabecalhoNfe cabecalhoNfe)
        {
            cabecalhoNfe.SetId(Guid.NewGuid());
            cabecalhoNfe.SetDataInclusao(DateTime.Now);

            if (!ValidateEntity(cabecalhoNfe)) return;

            if (!ValidateBusinessRules(new CabecalhoNfeCanAddValidation(_cabecalhoNfeRepository).Validate(cabecalhoNfe)))
                return;

            _cabecalhoNfeRepository.Add(cabecalhoNfe);
        }

        public bool TryManifestar(CabecalhoNfe cabecalhoNfe, out ManifestacaoNfeResult manifestacaoNfeResult, out MensagemErro mensagemErro)
        {
            manifestacaoNfeResult = null;
            mensagemErro = null;

            if (cabecalhoNfe.Notificacao.IsNullOrWhiteSpace())
            {
                var result = _notaFiscalIntegracaoManifestacao.TryManifestar(cabecalhoNfe, out manifestacaoNfeResult, out mensagemErro);

                if (!result)
                {
                    cabecalhoNfe.Notificacao = mensagemErro.Mensagem.Left(100);
                    cabecalhoNfe.SetManifestacaoDestinatario(null);
                }
            }

            _cabecalhoNfeRepository.Update(cabecalhoNfe);

            return mensagemErro == null;
        }

        public void Update(CabecalhoNfe cabecalhoNfe)
        {
            _cabecalhoNfeRepository.Update(cabecalhoNfe);
        }

        public void Delete(CabecalhoNfe cabecalhoNfe)
        {
            if (!ValidateBusinessRules(new CabecalhoNfeCanDeleteValidation(_cabecalhoNfeRepository).Validate(cabecalhoNfe)))
                return;

            _cabecalhoNfeRepository.Delete(cabecalhoNfe);
        }

        public void Dispose()
        {
            _cabecalhoNfeRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

        public Paged<CabecalhoNfe> GetUnmanifested(int pageNumber, int pageSize)
        {
            return _cabecalhoNfeRepository.GetUnmanifested(pageNumber, pageSize);
        }

        public bool TryGetUnmanifestedFromIntegration(out IEnumerable<CabecalhoNfeDto> cabecalhosNfes, 
                                                      out MensagemErro mensagemErro,
                                                      int VersaoMax)
        {
            if (!_notaFiscalIntegracaoManifestacao.TryGetUnmanifestedFromIntegration(out cabecalhosNfes, out mensagemErro, VersaoMax))
                return false;

            return mensagemErro == null;
        }

        public bool TryGetXmlNfeFromIntegrationByChave(string chaveNfe, out string xmlNfe, out MensagemErro mensagemErro)
        {
            if (!_notaFiscalIntegracaoManifestacao.TryGetXmlNfeFromIntegrationByChave(chaveNfe, out xmlNfe, out mensagemErro))
                return false;

            return mensagemErro == null;
        }

        public bool TryConsultar(string numeroNota, out ConsultaNfeIntegracao consultaNfeIntegracao, out MensagemErro mensagemErro, bool completa = false)
        {
            if (!_notaFiscalIntegracaoManifestacao.TryConsultar(numeroNota, out consultaNfeIntegracao, out mensagemErro, completa))
                return false;

            return mensagemErro == null;
        }

        public bool TryObterArquivo(string numeroNota, out byte[] conteudoArquivo, out MensagemErro mensagemErro)
        {
            if (!_notaFiscalIntegracaoManifestacao.TryObterArquivo(numeroNota, out conteudoArquivo, out mensagemErro))
                return false;

            return mensagemErro == null;
        }
    }
}

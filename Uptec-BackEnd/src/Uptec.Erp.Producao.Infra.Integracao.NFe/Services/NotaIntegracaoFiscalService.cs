using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Uptec.Erp.Infra.HttpClient;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Dtos.Integracao.Receita;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Integracao.Receita;
using Uptec.Erp.Producao.Infra.Integracao.NFe.Mappers;
using Uptec.Erp.Shared.Domain.Enums.NFe;
using Uptec.Erp.Shared.Domain.Enums.NFe.Processamento;
using Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.ConsultaNFe;
using Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.Erros;

namespace Uptec.Erp.Producao.Infra.Integracao.NFe.Services
{
    public class NotaFiscalIntegracaoService : INotaFiscalIntegracaoService
    {
        public readonly string _urlRaiz;
        public readonly string _token;
        public readonly IConfiguration _configuration;

        public NotaFiscalIntegracaoService(IConfiguration configuration)
        {
            _configuration = configuration;
            _urlRaiz = ObterConfiguracao("ApiNfe", "UrlRaiz");
            _token = ObterConfiguracao("ApiNfe", "ApiToken");
        }

        public bool TryEnviar(NotaSaida notaSaida, out MensagemErro mensagemErro)
        {
            var notaEnviar = notaSaida.ToIntegracao();

            var url = $"{_urlRaiz}{ObterConfiguracao("ApiNfe", "UrlEnvioNota").Replace("<REFERENCIA>", notaSaida.NumeroNota)}";

            if (url.Contains("homologacao"))
                notaEnviar.nome_destinatario = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";

            var response = HttpRequestFactory.Post(url, notaEnviar, _token).Result;
            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                mensagemErro = response.ContentAsType<MensagemErro>();
                return false;
            }

            mensagemErro = null;
            return true;
        }

        public bool TryConsultar(string numeroNota, out ConsultaNfeIntegracao consultaNfeIntegracao,
                                 out MensagemErro mensagemErro, bool completa = false)
        {
            var url = $"{_urlRaiz}{ObterConfiguracao("ApiNfe", "UrlConsultaNota").Replace("<REFERENCIA>", numeroNota)}?completa={(completa ? 1 : 0)}";

            var response = HttpRequestFactory.Get(url, _token);

            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                mensagemErro = response.Result.ContentAsType<MensagemErro>();
                consultaNfeIntegracao = null;
                return false;
            }

            mensagemErro = null;
            consultaNfeIntegracao = JsonConvert.DeserializeObject<ConsultaNfeIntegracao>(response.Result.Content.ReadAsStringAsync().Result);

            return true;
        }

        public bool Cancelar(string numeroNota)
        {
            var url = $"{_urlRaiz}{ObterConfiguracao("ApiNfe", "UrlCancelamentoNota")}";

            throw new NotImplementedException();
        }

        public bool TryObterArquivo(string CaminhoArquivo, out byte[] conteudoArquivo, out MensagemErro mensagemErro)
        {
            var response = HttpRequestFactory.Get($"{_urlRaiz}/{CaminhoArquivo}");

            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                mensagemErro = response.Result.ContentAsType<MensagemErro>(); //TODO testar eventuas erros
                conteudoArquivo = null;
                return false;
            }

            mensagemErro = null;

            conteudoArquivo = response.Result.Content.ReadAsByteArrayAsync().Result;
            return true;
        }

        public bool TryGetUnmanifestedFromIntegration(out IEnumerable<CabecalhoNfeDto> cabecalhosNfes, 
                                                      out MensagemErro mensagemErro, 
                                                      int VersaoMax)
        {
            var url = $"{_urlRaiz}{ObterConfiguracao("ApiNfe", "UrlManifestacaoNfeRecebida").Replace("<REFERENCIA>", "08059371000150")}".Replace("<VERSAO>", VersaoMax.ToString());

            var response = HttpRequestFactory.Get(url, _token);

            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                mensagemErro = response.Result.ContentAsType<MensagemErro>();
                cabecalhosNfes = null;
                return false;
            }

            mensagemErro = null;
            
            var jsonResult = response.Result.Content.ReadAsStringAsync().Result;

            cabecalhosNfes = JsonConvert.DeserializeObject<IEnumerable<CabecalhoNfeDto>>(jsonResult)
                                .ToList()
                                .Where(n => n.Situacao == SituacaoNfe.Autorizada);

            return true;
        }

        public bool TryGetXmlNfeFromIntegrationByChave(string chaveNfe,
                                                       out string xmlNfe,
                                                       out MensagemErro mensagemErro)
        {
            var url = $"{_urlRaiz}{ObterConfiguracao("ApiNfe", "UrlManifestacaoNfeRecebidaPorChave").Replace($"<REFERENCIA>", $"{chaveNfe}")}";

            var response = HttpRequestFactory.Get(url, _token);

            if (response.Result.StatusCode != HttpStatusCode.OK)
            {
                mensagemErro = response.Result.ContentAsType<MensagemErro>();
                xmlNfe = null;
                return false;
            }

            mensagemErro = null;
            xmlNfe = response.Result.Content.ReadAsStringAsync().Result;

            return true;
        }

        public bool TryManifestar(CabecalhoNfe cabecalhoNfe, out ManifestacaoNfeResult manifestacaoNfeResult, out MensagemErro mensagemErro)
        {
            var notaManifestacao = new ManifestacaoNfeDto
            {
                justificativa = cabecalhoNfe.JustificativaManifestacao,
                tipo = ObterStatusManifestacao(cabecalhoNfe.ManifestacaoDestinatario)
            };

            var url = $"{_urlRaiz}{ObterConfiguracao("ApiNfe", "UrlManifestacao").Replace("<REFERENCIA>", cabecalhoNfe.ChaveNfe)}";

            var response = HttpRequestFactory.Post(url, notaManifestacao, _token).Result;

            if (response.StatusCode != HttpStatusCode.OK)
            {
                manifestacaoNfeResult = null;
                mensagemErro = response.ContentAsType<MensagemErro>();
                return false;
            }

            manifestacaoNfeResult = JsonConvert.DeserializeObject<ManifestacaoNfeResult>(response.Content.ReadAsStringAsync().Result);

            if (manifestacaoNfeResult.Status == "erro")
            {
                mensagemErro = new MensagemErro { Mensagem = manifestacaoNfeResult.Mensagem_sefaz};
                return false;
            }

            mensagemErro = null;
            return true;
        }

        private string ObterConfiguracao(string secao, string chave)
        {
            return _configuration.GetSection(secao).GetSection(chave).Value;
        }

        private string ObterStatusManifestacao(ManifestacaoStatus? manifestacaoStatus)
        {
            switch (manifestacaoStatus)
            {
                case ManifestacaoStatus.Ciencia:
                    return "ciencia";
                case ManifestacaoStatus.Confirmacao:
                    return "confirmacao";
                case ManifestacaoStatus.Desconhecimento:
                    return "desconhecimento";
                case ManifestacaoStatus.NaoRealizada:
                default:
                    return "nao_realizada";
            }
        }
    }
}

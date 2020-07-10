using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Uptec.Erp.Producao.Domain.Fiscal.Events;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Dtos;
using Uptec.Erp.Producao.Domain.Ordens.Interfaces;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Impostos;
using Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.ConsultaNFe;
using Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.Erros;
using Definitiva.Shared.Infra.Support.Helpers;

namespace Uptec.Erp.Producao.Domain.Fiscal.Services
{
    public class NotaSaidaService : BaseService, INotaSaidaService
    {
        private readonly INotaSaidaRepository _notaSaidaRepository;
        private readonly IOrdemService _ordemService;
        private readonly INotaSaidaEmissao _notaSaidaEmissao;
        private readonly INotaFiscalSaidaIntegracao _notaFiscalSaidaIntegracao;

        public NotaSaidaService(IBus bus,
                                INotaSaidaRepository fiscalRepository,
                                IOrdemService ordemService,
                                INotaSaidaEmissao notaSaidaEmissao,
                                INotaFiscalSaidaIntegracao notaFiscalSaidaIntegracao
                                ) : base(bus)
        {
            _notaSaidaRepository = fiscalRepository;
            _ordemService = ordemService;
            _notaSaidaEmissao = notaSaidaEmissao;
            _notaFiscalSaidaIntegracao = notaFiscalSaidaIntegracao;
        }

        public NotaSaida GetWithUpdateStatusSefaz(Guid id, out bool hasChanges)
        {
            var nota = _notaSaidaRepository.GetByIdWithAggregate(id);

            if (nota == null) { hasChanges = false; return null; }

            if (nota.Status != StatusNfSaida.Processando) { hasChanges = false; return nota; }
                
            if (TryConsultar(nota.NumeroNota, out var consultaNfeIntegracao, out var mensagemErroConsulta))
                SetNotaIntegracaoStatus(consultaNfeIntegracao, nota);
            else
            {
                if(mensagemErroConsulta.Mensagem == "Nota fiscal não encontrada")
                    nota.SetStatus(StatusNfSaida.Rejeitada, "Erro inesperado API, tente enviar novamente");
                else
                    nota.SetStatus(nota.Status, mensagemErroConsulta.Mensagem);
            }
            _notaSaidaRepository.Update(nota);

            hasChanges = true;
            return nota;
        }

        public NotaSaidaAddedEvent Add(NotaSaidaAddDto notaSaidaDto)
        {
            var nota = _notaSaidaEmissao.EmitirNota(notaSaidaDto);

            if (nota == null) return null;

            _notaSaidaRepository.Add(nota);

            if (notaSaidaDto.OrdemItens.Any())
                UpdateOrdem(nota, notaSaidaDto.OrdemItens);

            return (new NotaSaidaAddedEvent(nota.Id));
        }

        public void Update(NotaSaida notaSaida)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool Cancelar(string notaSaida, out MensagemErro mensagemErro)
        {
            return _notaFiscalSaidaIntegracao.Cancelar(notaSaida, out mensagemErro);
        }

        public bool TryConsultar(string numeroNota, out ConsultaNfeIntegracao consultaNfeIntegracao, out MensagemErro mensagemErro, bool completa = false)
        {
            return _notaFiscalSaidaIntegracao.TryConsultar(numeroNota, out consultaNfeIntegracao, out mensagemErro);
        }

        public bool TryEnviar(NotaSaida notaSaida, out MensagemErro mensagemErro)
        {
            return _notaFiscalSaidaIntegracao.TryEnviar(notaSaida, out mensagemErro);
        }

        public NotaSaida Reenviar(Guid notaId, out bool hasChanges)
        {
            var nota = _notaSaidaRepository.GetByIdWithAggregate(notaId);

            if (nota == null)
            {
                NotifyError("Nota inexistente");
                hasChanges = false;
                return null;
            }

            nota.SetEnderecoDestinatario(_notaSaidaRepository.GetEndereco(nota.EnderecoId, nota.TipoDestinatario));

            if (nota.TransportadoraId != null)
                nota.SetEnderecoTransportadora(_notaSaidaRepository.GetEnderecoTransportadora(nota.TransportadoraId.Value));

            var retorno = _notaFiscalSaidaIntegracao.TryEnviar(nota, out var mensagemErro);

            if (!retorno)
                nota.SetStatus(StatusNfSaida.Rejeitada, $"{DateTime.Now.Date.ToString()} - {mensagemErro.Codigo} - {mensagemErro.Mensagem}");
            else
                nota.SetStatus(StatusNfSaida.Processando, "");

            _notaSaidaRepository.Update(nota);
            hasChanges = true;
            return nota;
        }

        public AliquotaImpostos GetAliquotaImpostos(string uf)
        {
            return _notaSaidaEmissao.GetAliquotaImpostos(uf);
        }

        public bool TryObterArquivo(string numeroNota, out byte[] conteudoArquivo,
                                    out MensagemErro mensagemErro, TipoArquivo tipoArquivo)
        {
            if (!_notaFiscalSaidaIntegracao.TryConsultar(numeroNota, out var consulta, out var msgErro))
            {
                mensagemErro = msgErro;
                conteudoArquivo = null;
                return false;
            }

            if (!_notaFiscalSaidaIntegracao.TryObterArquivo(
                tipoArquivo == TipoArquivo.Pdf ? consulta.Caminho_Danfe : consulta.Caminho_Xml_Nota_Fiscal,
                out var conteudo, out var msgErroDownload))
            {
                mensagemErro = msgErroDownload;
                conteudoArquivo = null;
                return false;
            }

            conteudoArquivo = conteudo;
            mensagemErro = msgErroDownload;
            return true;
        }

        public bool TryChangeStatusProcessamento(ConsultaNfeIntegracao consultaNfeIntegracao)
        {
            if (consultaNfeIntegracao == null)
            {
                NotifyError("Retorno de consulta est� nulo.");
                return false;
            }

            if (consultaNfeIntegracao.Numer.IsNullOrWhiteSpace())
            {
                NotifyError("O n�mero da nota n�o foi informado.");
                return false;
            }

            var nota = _notaSaidaRepository.GetByNumeroNotaWithAggregate(consultaNfeIntegracao.Numer);

            if (nota == null)
            {
                NotifyError("Nota inexistente");
                return false;
            }

            if (nota.Status == StatusNfSaida.Processada)
                return false;

            SetNotaIntegracaoStatus(consultaNfeIntegracao, nota);

            _notaSaidaRepository.Update(nota);

            return true;
        }

        private void UpdateOrdem(NotaSaida notaSaida, List<NotaSaidaOrdemItensDto> ordemItens)
        {
            if (ordemItens.Any())
            {
                foreach (var item in ordemItens)
                    _ordemService.SetNotaSaida(notaSaida.NumeroNota, item.OrdemLoteId);
            }
        }

        private void SetNotaIntegracaoStatus(ConsultaNfeIntegracao consultaNfeIntegracao, NotaSaida notaSaida)
        {
            switch (consultaNfeIntegracao.Status)
            {
                case "processando_autorizacao":
                    notaSaida.SetStatus(StatusNfSaida.Processando, string.Empty);
                    break;
                case "autorizado":
                    notaSaida.SetStatus(StatusNfSaida.Processada, string.Empty);
                    break;
                default:
                    notaSaida.SetStatus(StatusNfSaida.Rejeitada, 
                        $"{DateTime.Now.Date} - {consultaNfeIntegracao.Status_Sefaz} - {consultaNfeIntegracao.Mensagem_Sefaz}");
                    break;
            }
        }

        public void Dispose()
        {
            _notaSaidaRepository?.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
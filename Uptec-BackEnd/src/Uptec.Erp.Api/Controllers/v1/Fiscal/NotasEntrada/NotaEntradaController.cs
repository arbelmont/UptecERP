using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.DomainNotifications;
using Definitiva.Shared.Domain.Interfaces;
using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Infra.Support.Helpers;
using Definitiva.Shared.Infra.Support.Tracker.Domain.Interfaces;
using ExpressMapper.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Uptec.Erp.Api.Controllers.v1.Fiscal.NotasEntrada.Domain.Services;
using Uptec.Erp.Api.ViewModels.Producao.Arquivos;
using Uptec.Erp.Api.ViewModels.Producao.NotasEntrada;
using Uptec.Erp.Api.ViewModels.Producao.NotasEntrada.SEFAZ;
using Uptec.Erp.Api.ViewModels.Shared;
using Uptec.Erp.Producao.Domain.Arquivos.Interfaces;
using Uptec.Erp.Producao.Domain.Arquivos.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Dtos.Integracao.Receita;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Integracao.Receita;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Enums.NFe;
using Uptec.Erp.Shared.Domain.Enums.NFe.Processamento;

namespace Uptec.Erp.Api.Controllers.v1.Fiscal.NotasEntrada
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]

    public class NotaEntradaController : BaseController
    {
        private readonly IArquivoService _ArquiovoService;
        private readonly INotaEntradaService _notaEntradaService;
        private readonly INotaEntradaRepository _notaEntradaRepository;
        private readonly ICabecalhoNfeRepository _cabecalhoNfeRepository;
        private readonly ITrackerService _trackerService;
        private readonly IManifestacaoNfeService _manifestacaoService;
        private readonly IBus _bus;

        public NotaEntradaController(INotificationHandler<DomainNotification> notifications,
                              IArquivoService arquivoService,
                              INotaEntradaService notaEntradaService,
                              INotaEntradaRepository notaEntradaRepository,
                              ILoteRepository loteRepository,
                              IUnitOfWork uow, IBus bus,
                              ITrackerService trackerService,
                              IManifestacaoNfeService manifestacaiService,
                              ICabecalhoNfeRepository cabecalhoNfeRepository) : base(notifications, uow, bus)
        {
            _notaEntradaService = notaEntradaService;
            _ArquiovoService = arquivoService;
            _notaEntradaRepository = notaEntradaRepository;
            _bus = bus;
            _trackerService = trackerService;
            _manifestacaoService = manifestacaiService;
            _cabecalhoNfeRepository = cabecalhoNfeRepository;
        }

        [HttpPost]
        public IActionResult Post([FromForm] ArquivoAddViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            var arquivoXml = viewModel.Map<ArquivoAddViewModel, Arquivo>();

            return Response(ArquivoAdd(arquivoXml));
        }

        [HttpPut("Conciliar")]
        public IActionResult Conciliar([FromBody] NotaEntradaViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            var nota = _notaEntradaRepository.GetByIdWithAggregate(viewModel.Id);

            if (nota == null)
            {
                NotifyError("", "Nota Inexistente");
                return Response();
            }

            nota.SetDataConciliacao(DateTime.Now);

            if (nota.TipoEstoque == TipoEstoque.Peca)
                foreach (var item in nota.Itens)
                {
                    var itemViewModel = viewModel.Itens.Find(x => x.Id == item.Id);
                    item.SetDataFabricacao(itemViewModel.DataFabricacao.Value);
                    item.SetDataValidade(itemViewModel.DataValidade.Value);
                    item.SetLocalizacao(itemViewModel.Localizacao);
                    item.SetQtdeConcilia(itemViewModel.QtdeConcilia.Value);
                }

            _notaEntradaService.Conciliar(nota);

            Commit();

            return Response();
        }

        [HttpPut("Cobrir")]
        public IActionResult Cobrir([FromBody] NotaEntradaCoberturaViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _notaEntradaService.Cobrir(viewModel.NotaEntradaFornecedorId, viewModel.NotaEntradaClienteId);

            Commit();

            return Response();
        }

        [HttpPut("DefinirTipoEmissor")]
        public IActionResult DefinirTipoEmissor([FromBody] NotaEntradaTipoEmissorViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _notaEntradaService.DefinirTipoEmissor(viewModel.Id, viewModel.TipoEmissor);

            Commit();

            return Response();
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public PagedViewModel<NotaEntradaViewModel> Get(int pageNumber, int pageSize,
                [FromQuery] int tipoEmissor, [FromQuery] int status,
                [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string search)
        {
            if (startDate == DateTime.MinValue)
                startDate = DateTime.Now.AddMonths(-1);

            if (endDate == DateTime.MinValue)
                endDate = DateTime.Now;

            var retorno = _notaEntradaRepository.GetPaged(pageNumber, pageSize, tipoEmissor, status, startDate, endDate, search.ReplaceNull());

            return retorno.Map<Paged<NotaEntrada>, PagedViewModel<NotaEntradaViewModel>>();
        }

        [HttpGet("GetFullByIdConsisted/{id:Guid}")]
        public IActionResult GetByIdWithAggregateConsisted(Guid id)
        {
            var model = _notaEntradaService.GetConsistida(id).Map<NotaEntrada, NotaEntradaViewModel>();

            if (model.TipoEmissor == TipoEmissor.Cliente && model.TipoEstoque == TipoEstoque.Peca)
                model.QtdeNotasAcobrir = _notaEntradaRepository.GetQtdeNotasAcobrir();

            return ResponseIgnoreDomainErrors(model);
        }

        [HttpGet("GetNotasClientesConciliar")]
        public IActionResult GetNotasClientesConciliar()
        {
            var model = _notaEntradaRepository.GetNotasClientesConciliar().Map<List<NotaEntrada>, List<NotaEntradaViewModel>>(); ;

            var dropdownList = new List<DropDownViewModel>();

            model.ForEach(nota =>
            {
                var name = $"{nota.NomeEmissor.PadRight(25, ' ')} Cnpj: [{nota.CnpjEmissor.PadRight(15, ' ')}] NF: [{nota.NumeroNota.PadRight(15, ' ')}] Data: [{nota.Data.Date.ToString("dd/MM/yyyy")}] Valor: [{nota.Valor.ToString().Replace('.', ',').PadLeft(15, ' ')}]";
                dropdownList.Add(new DropDownViewModel { Value = nota.Id.ToString(), Name = name });
            }
            );

            return Response(dropdownList);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            _notaEntradaService.Delete(id);

            Commit();

            return Response();
        }

        [AllowAnonymous]
        [HttpPost("GravarNotaFiscalManifestada")]
        public IActionResult GravarNotaFiscalManifestada(NotaEntradaStatusProcessamentoViewModel viewModel)
        {
            try
            {
                _trackerService.Trace("Recebimento de nota manifestada - FocusAPI (Sem Validação)", JsonConvert.SerializeObject(viewModel));

                if (!IsValidModelState() || viewModel == null)
                    return Response();

                if (!_manifestacaoService.TryGetXmlNfeFromIntegrationByChave(viewModel.Chave_nfe, out var xmlNfe, out var mensagemErroXml))
                {
                    _trackerService.Trace("Recebimento de nota manifestada - FocusAPI (método TryGetXmlNfeFromIntegrationByChave)", $"Erro ao consultar o XML | Mensagem: {mensagemErroXml.Codigo}-{ mensagemErroXml.Mensagem}");
                    return Response();
                }

                ArquivoAdd(new Arquivo(Guid.NewGuid(), $"{viewModel.Chave_nfe}.xml", xmlNfe.Length, "text/xml", xmlNfe));

                return Response();
            }
            catch (Exception ex)
            {
                _trackerService.Trace("Recebimento de nota manifestada - FocusAPI (método TryObterArquivo | Exception)", $"Exception: {ex.Message}");
            }

            return Response();
        }

        [HttpGet("GetUnmanifested/{pageNumber:int}/{pageSize:int}")]
        public IActionResult GetUnmanifested(int pageNumber, int pageSize)
        {
            var result = _manifestacaoService.GetUnmanifested(pageNumber, pageSize);
            return Response(result);
        }

        [HttpPost("ManifestarNfe")]
        public IActionResult ManifestarNfe(IEnumerable<CabecalhoNfeManifestacaoViewModel> viewModel)
        {
            if (viewModel == null)
                return null;

            var cabecalhos = viewModel.ToList().Map<List<CabecalhoNfeManifestacaoViewModel>, List<CabecalhoNfeManifestacaoViewModelResult>>();

            foreach (var cabecalho in cabecalhos)
            {
                if (cabecalho.ManifestacaoDestinatario == null)
                {
                    cabecalho.Notificacao = $"Status da Manifestação inválido ou não informado. Chave {cabecalho.ChaveNfe}.";
                    continue;
                }

                var cabecalhoNota = _cabecalhoNfeRepository.GetById(cabecalho.Id);
                cabecalhoNota.Notificacao = null;

                if (cabecalhoNota == null)
                {
                    cabecalho.Notificacao = $"Chave não foi localizada na base. Chave {cabecalho.ChaveNfe}";
                    continue;
                }

                if (cabecalho.ManifestacaoDestinatario == ManifestacaoStatus.NaoRealizada
                    && cabecalho.Justificativa.IsNullOrWhiteSpace())
                    cabecalho.Notificacao = "Necessário justificar o motivo da não realização da manifestação da nota fiscal.";

                cabecalhoNota.SetManifestacaoDestinatario(cabecalho.ManifestacaoDestinatario);
                cabecalhoNota.SetJustificativaManifestacao(cabecalho.Justificativa);

                if (!_manifestacaoService.TryManifestar(cabecalhoNota, out var manifestacaoNfeResult, out var mensagemErro))
                    cabecalho.Notificacao = mensagemErro.Mensagem;

                Commit();
            }

            return Response();
        }

        [HttpGet("GetUnmanifestedFromIntegration")]
        public IActionResult GetUnmanifestedFromIntegration()
        {
            _manifestacaoService.TryGetUnmanifestedFromIntegration(out var cabecalhosNfes, 
                                                                   out var mensagemErro,
                                                                   _cabecalhoNfeRepository.ObterVersaoMax());

            var nfePreValidadas = cabecalhosNfes.GroupBy(c => new { c.Chave_nfe, c.Situacao }).Select(c => c.First()).ToList();
            var cabecalhos = nfePreValidadas.Map<List<CabecalhoNfeDto>, List<CabecalhoNfe>>();

            foreach (var cabecalho in cabecalhos)
            {
                if (cabecalho.Situacao == SituacaoNfe.Autorizada)
                    _manifestacaoService.Add(cabecalho);
                else
                    _manifestacaoService.Delete(cabecalho);

                CommitUntracked();
            }

            return ResponseIgnoreDomainErrors();
        }

        private Guid ArquivoAdd(Arquivo arquivoXml)
        {
            var notaEntradaViewModel = new NotaEntradaAppService(_bus).EntrairDadosNotaEntrada(arquivoXml.Dados);
            notaEntradaViewModel.EmailEmissor = ""; //TODO if necessary (by Marcone) 

            _ArquiovoService.Add(arquivoXml);

            notaEntradaViewModel.Id = Guid.NewGuid();
            foreach (var item in notaEntradaViewModel.Itens)
            {
                item.Id = Guid.NewGuid();
                item.NotaEntradaId = notaEntradaViewModel.Id;
            }

            var notaEntrada = notaEntradaViewModel.Map<NotaEntradaViewModel, NotaEntrada>();
            notaEntrada.SetArquivoId(arquivoXml.Id);

            if (arquivoXml.IsValid())
                _notaEntradaService.Add(notaEntrada);

            Commit();

            return notaEntrada.Id;
        }
    }
}

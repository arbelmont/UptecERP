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
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Uptec.Erp.Api.ViewModels.Producao.NotasSaida;
using Uptec.Erp.Api.ViewModels.Shared;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Dtos;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.ConsultaNFe;

namespace Uptec.Erp.Api.Controllers.v1.Fiscal.NotasSaida
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    //[Authorize]
    public class NotaSaidaController : BaseController
    {
        private readonly INotaSaidaService _notaSaidaService;
        private readonly INotaSaidaEmissao _notaSaidaEmissao;
        private readonly INotaSaidaRepository _notaSaidaRepository;
        private readonly IConfiguration _configuration;
        private readonly ITrackerService _trackerService;

        public NotaSaidaController(INotificationHandler<DomainNotification> notifications,
                              INotaSaidaService notaSaidaService,
                              INotaSaidaEmissao notaSaidaEmissao,
                              INotaSaidaRepository notaSaidaRepository,
                              IUnitOfWork uow, IBus bus, IConfiguration configuration,
                              ITrackerService trackerService) : base(notifications, uow, bus)
        {
            _notaSaidaService = notaSaidaService;
            _notaSaidaEmissao = notaSaidaEmissao;
            _notaSaidaRepository = notaSaidaRepository;
            _configuration = configuration;
            _trackerService = trackerService;
        }

        [HttpGet("GetFullById/{id:guid}")]
        public IActionResult GetFullById(Guid id)
        {
            var nota = _notaSaidaRepository.GetByIdWithAggregate(id).Map<NotaSaida, NotaSaidaViewModel>();

            nota.Itens.OrderBy(i => i.TipoItem);

            if (nota == null)
            {
                NotifyError("", "Nota Inexistente");
                return Response();
            }

            nota.EnderecoCompleto = _notaSaidaRepository.GetEndereco(nota.EnderecoId, nota.TipoDestinatario).EnderecoCompleto;

            return Response(nota);
        }

        [HttpGet("GeWithStatusSefaz/{id:guid}")]
        public IActionResult GetWithStatusSefaz(Guid id)
        {
            var nota = _notaSaidaService.GetWithUpdateStatusSefaz(id, out var hasChanges).Map<NotaSaida, NotaSaidaViewModel>();

            nota.Itens = nota.Itens.OrderBy(i =>  i.LoteNumero + i.LoteSequencia).ThenBy(i => (int)i.TipoItem).ToList();

            if (nota == null)
            {
                NotifyError("", "Nota Inexistente");
                return Response();
            }

            if (hasChanges)
                Commit();

            nota.EnderecoCompleto = _notaSaidaRepository.GetEndereco(nota.EnderecoId, nota.TipoDestinatario).EnderecoCompleto;

            return Response(nota);
        }

        [HttpGet("Reenviar/{id:guid}")]
        public IActionResult Reenviar(Guid id)
        {
            var nota = _notaSaidaService.Reenviar(id, out var hasChanges).Map<NotaSaida, NotaSaidaViewModel>();

            if (nota == null)
                return Response();

            if (hasChanges)
                Commit();

            nota.EnderecoCompleto = _notaSaidaRepository.GetEndereco(nota.EnderecoId, nota.TipoDestinatario).EnderecoCompleto;

            return Response(nota);
        }

        [HttpPost("Enviar")]
        public IActionResult Enviar(string numeroNota)
        {
            if (!IsValidModelState()) return Response();

            var notaSaida = _notaSaidaRepository.GetByNumeroNotaWithAggregate(numeroNota);

            //var consulta = _notaSaidaService.TryConsultar("61502", out var consultaNfeIntegracao, out var mensagemErroConsulta);

            if (!_notaSaidaService.TryEnviar(notaSaida, out var result))
                return Response(result);

            return Response();
        }

        [HttpDelete("CancelSefaz/{id:guid}")]
        public IActionResult CancelSefaz(Guid id)
        {
            if (!IsValidModelState()) return Response();

            var notaSaida = _notaSaidaRepository.GetByIdWithAggregate(id);

            if (notaSaida !=null) { 

                if (!_notaSaidaService.Cancelar(notaSaida.NumeroNota, out var result))
                    return Response(result);
            }
            
            notaSaida.SetStatus(StatusNfSaida.Cancelada, "");

            _notaSaidaRepository.UpdateStatus(notaSaida);

            return Response();
        }

        [HttpPost]
        public IActionResult Post([FromBody] NotaSaidaAddViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            var dtoService = viewModel.Map<NotaSaidaAddViewModel, NotaSaidaAddDto>();

            StartTransaction();

            var notaAdded = _notaSaidaService.Add(dtoService);

            if (Commit())
            {
                StartTransaction();
                PublishEvent(notaAdded);
                CommitTransactionUntracked();
            }

            return Response();
        }

        [HttpPost("GerarOutrasInformacoes")]
        public IActionResult GetOutrasInformacoes([FromBody] NotaSaidaAddViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            var dtoService = viewModel.Map<NotaSaidaAddViewModel, NotaSaidaAddDto>();

            var xx = _notaSaidaEmissao.GetOutrasInformacoes(dtoService);

            return Response(xx);
        }

        [HttpGet("AliquotaImpostos")]
        public IActionResult GetAliquotaImpostos(string uf)
        {
            return Response(_notaSaidaService.GetAliquotaImpostos(uf));
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public PagedViewModel<NotaSaidaViewModel> Get(int pageNumber, int pageSize,
                [FromQuery] int tipoDestinatario, [FromQuery] int status,
                [FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] string search)
        {
            if (startDate == DateTime.MinValue)
                startDate = DateTime.Now.AddMonths(-1);

            if (endDate == DateTime.MinValue)
                endDate = DateTime.Now;

            var retorno = _notaSaidaRepository.GetPaged(pageNumber, pageSize, tipoDestinatario, status, startDate, endDate, search.ReplaceNull(""));

            return retorno.Map<Paged<NotaSaida>, PagedViewModel<NotaSaidaViewModel>>();
        }

        [HttpGet("DownloadPdf")]
        public IActionResult DownloadPdf(string numeroNota)
        {
            if (_notaSaidaService.TryObterArquivo(numeroNota, out var arquivo, out var mensagemErro, TipoArquivo.Pdf))
            {
                return File(arquivo, fileDownloadName: $"{numeroNota}.pdf", contentType: "application/octet-stream");
            }

            NotifyError("", mensagemErro.Mensagem);
            return Response();

        }

        [HttpGet("DownloadXml")]
        public IActionResult DownloadXml(string numeroNota)
        {
            if (_notaSaidaService.TryObterArquivo(numeroNota, out var arquivo, out var mensagemErro, TipoArquivo.Xml))
            {
                return File(arquivo, fileDownloadName: $"{numeroNota}.xml", contentType: "application/octet-stream");
            }

            NotifyError("", mensagemErro.Mensagem);
            return Response();

        }

        [AllowAnonymous]
        [HttpPost("StatusProcessamentoNfe/nfe")]
        public IActionResult StatusProcessamentoNfe(NotaSaidaStatusProcessamentoViewModel viewModel)
        {
            _trackerService.Trace("Retorno FocusAPI (Sem Validação) | VM", JsonConvert.SerializeObject(viewModel));

            if (!IsValidModelState())
            {
                _trackerService.Trace("Retorno FocusAPI (IsValidModelState = False) | VM", JsonConvert.SerializeObject(viewModel));
                return Response();
            }

            _trackerService.Trace("Retorno FocusAPI (IsValidModelState = TRUE) | VM", JsonConvert.SerializeObject(viewModel));

            var consultaNfeIntegracao = viewModel.Map<NotaSaidaStatusProcessamentoViewModel, ConsultaNfeIntegracao>();

            var jsonMapeamento = JsonConvert.SerializeObject(consultaNfeIntegracao);

            _trackerService.Trace("Retorno FocusAPI (IsValidModelState = TRUE) | VM", JsonConvert.SerializeObject(viewModel) + $" | JsonMapeamento: {jsonMapeamento}");

            var result = _notaSaidaService.TryChangeStatusProcessamento(consultaNfeIntegracao);

            _trackerService.Trace("Retorno FocusAPI (Atualizado com sucesso) | VM", JsonConvert.SerializeObject(viewModel));

            if (result)
                Commit();

            return Response();
        }

        [AllowAnonymous]
        [HttpGet("VerificacaoStatus")]
        public IActionResult VerificacaoStatus()
        {
            var result = new List<string>
            {
                _configuration.GetSection("ApiNfe").GetSection("UrlRaiz").Value,
                _configuration.GetSection("ApiNfe").GetSection("UrlEnvioNota").Value,
                _configuration.GetSection("ApiNfe").GetSection("UrlConsultaNota").Value,
                _configuration.GetSection("ApiNfe").GetSection("UrlCancelamentoNota").Value,
                _configuration.GetSection("ApiNfe").GetSection("UrlEmail").Value,
                _configuration.GetSection("ApiNfe").GetSection("UrlCartaCorrecaoNota").Value,
                _configuration.GetSection("ApiNfe").GetSection("UrlInutilizacao").Value
            };

            return Response(result);
        }
    }
}

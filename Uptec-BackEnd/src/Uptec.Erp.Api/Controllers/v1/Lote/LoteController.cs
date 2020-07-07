using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.DomainNotifications;
using Definitiva.Shared.Domain.Interfaces;
using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Infra.Support.Helpers;
using ExpressMapper.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Uptec.Erp.Api.ViewModels.Producao.Lotes;
using Uptec.Erp.Api.ViewModels.Shared;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Shared.Domain.Enums;
using LoteAlias = Uptec.Erp.Producao.Domain.Lotes.Models.Lote;

namespace Uptec.Erp.Api.Controllers.v1.Lote
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class LoteController : BaseController
    {
        private readonly ILoteService _loteService;
        private readonly ILoteRepository _loteRepository;

        public LoteController(INotificationHandler<DomainNotification> notifications,
                              ILoteService loteService,
                              ILoteRepository loteRepository,
                              IUnitOfWork uow,
                              IBus bus) : base(notifications, uow, bus)
        {
            _loteService = loteService;
            _loteRepository = loteRepository;
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            return Response(_loteRepository.GetById(id).Map<LoteAlias, LoteViewModel>());
        }

        [HttpGet("GetFullById/{id:Guid}")]
        public IActionResult GetByIdWithAggregate(Guid id)
        {
            try
            {
                var lote = _loteRepository.GetByIdWithAggregate(id);
                var loteViewModel = lote.Map<LoteAlias, LoteViewModel>();
                return Response(loteViewModel);

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpGet("{pageNumber:int}/{pageSize:int}/{showLoteFechado:bool}")]
        public PagedViewModel<LoteViewModel> Get(int pageNumber, int pageSize, bool showLoteFechado, [FromQuery] string search)
        {
            return _loteRepository.GetPaged(pageNumber, pageSize, showLoteFechado, search.ReplaceNull())
                .Map<Paged<LoteAlias>, PagedViewModel<LoteViewModel>>();
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}/{pecaId:guid}/{showLoteFechado:bool}")]
        public PagedViewModel<LoteViewModel> Get(int pageNumber, int pageSize, Guid pecaId, bool showLoteFechado)
        {
            return _loteRepository.GetPaged(pageNumber, pageSize, pecaId, showLoteFechado)
                .Map<Paged<LoteAlias>, PagedViewModel<LoteViewModel>>();
        }

        [HttpGet("GetFullPagedByPeca/{pageNumber:int}/{pageSize:int}/{pecaId:guid}/{showLoteFechado:bool}")]
        public PagedViewModel<LoteViewModel> GetFullPagedByPeca(int pageNumber, int pageSize, Guid pecaId, bool showLoteFechado)
        {
            return _loteRepository.GetFullPagedByPeca(pageNumber, pageSize, pecaId, showLoteFechado)
                .Map<Paged<LoteAlias>, PagedViewModel<LoteViewModel>>();
        }

        [HttpGet("GetLotesByNota")]
        public IActionResult GetLotesByNota([FromQuery] string numeroNota)
        {
            var lotes = _loteRepository.GetByNumeroNotaOuCobertura(numeroNota).ToList().Map<List<LoteAlias>, List<LoteViewModel>>();

            return Response(lotes);
        }

        [HttpGet("GetFullLotesByNota")]
        public IActionResult GetFullLotesByNota([FromQuery] string numeroNota)
        {
            var lotes = _loteRepository.GetByNumeroNotaOuCoberturaWithAggregate(numeroNota).ToList().Map<List<LoteAlias>, List<LoteViewModel>>();

            return Response(lotes);
        }

        [HttpGet("GetLoteSequenceLastUsed")]
        public IActionResult GetLoteSequenceLastUsed()
        {
            return Response(_loteRepository.GetLoteSequenceLastUsed() + 1);
        }

        [HttpGet("GetLoteDadosSaida/{id:guid}")]
        public IActionResult GetLoteDadosSaida(Guid id)
        {
            return Response(_loteService.GetLoteDadosSaida(id).Map<LoteDadosSaida, LoteDadosSaidaViewModel>());
        }

        [HttpGet("GetLotesEmbalagemNfeSaida/{destinatarioId:guid}/{tipoDestinatario:int}")]
        public IActionResult GetLotesEmbalagemToNfeSaida(Guid destinatarioId, TipoDestinatario tipoDestinatario)
        {
            var ret = _loteRepository.GetLotesEmbalagemToNfeSaida(destinatarioId, tipoDestinatario).ToList().Map<List<LoteAlias>, List<LoteViewModel>>();
            return Response(ret);
        }

        [HttpGet("GetLotesPecaNfeSaida/{destinatarioId:guid}/{tipoDestinatario:int}")]
        public IActionResult GetLotesPecaToNfeSaida(Guid destinatarioId, TipoDestinatario tipoDestinatario)
        {
            var ret = _loteRepository.GetLotesPecaToNfeSaida(destinatarioId, tipoDestinatario).ToList().Map<List<LoteAlias>, List<LoteViewModel>>();
            return Response(ret);
        }

        [HttpGet("Saldos")]
        public IActionResult GetSaldos()
        {
            var linha = _loteRepository.GetLotesSaldo()
                .ToList().Map<List<LoteSaldo>, List<LoteSaldoViewModel>>();

            return Response(linha);
        }
        #region Movimentos

        [HttpGet("Movimentos/{id:guid}")]
        public IActionResult GetMovimento(Guid id)
        {
            return Response(_loteRepository.GetMovimentoById(id).Map<LoteMovimento, LoteMovimentoViewModel>());
        }

        [HttpGet("Movimentos/{pageNumber:int}/{pageSize:int}")]
        public PagedViewModel<LoteMovimentoViewModel> Get(int pageNumber, int pageSize, [FromQuery] Guid loteId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate == DateTime.MinValue)
                startDate = DateTime.Now.AddMonths(-1);

            if (endDate == DateTime.MinValue)
                endDate = DateTime.Now;

            return _loteRepository.GetPagedMovimento(loteId, startDate, endDate, pageNumber, pageSize)
                .Map<Paged<LoteMovimento>, PagedViewModel<LoteMovimentoViewModel>>();
        }

        [HttpGet("Movimentos/{pageNumber:int}/{pageSize:int}/{loteId:guid}")]
        public PagedViewModel<LoteMovimentoViewModel> Get(int pageNumber, int pageSize, Guid loteId)
        {
            return _loteRepository.GetPagedMovimento(loteId, pageNumber, pageSize)
                .Map<Paged<LoteMovimento>, PagedViewModel<LoteMovimentoViewModel>>();
        }

        [HttpPost("Movimentos")]
        public IActionResult Post([FromBody] LoteAddMovimentoViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            if (viewModel.TipoMovimento == TipoMovimentoEstoque.Entrada)
                _loteService.AddMovimentoEntrada(viewModel.Map<LoteAddMovimentoViewModel, LoteMovimento>());
            else
                _loteService.AddMovimentoSaida(viewModel.Map<LoteAddMovimentoViewModel, LoteMovimento>());

            Commit();

            return Response();
        }
        #endregion
    }
}
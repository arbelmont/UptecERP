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
using Uptec.Erp.Api.ViewModels.Producao.Ordem;
using Uptec.Erp.Api.ViewModels.Shared;
using Uptec.Erp.Producao.Domain.Ordens.Interfaces;
using Uptec.Erp.Producao.Domain.Ordens.Models;
using OrdemAlias = Uptec.Erp.Producao.Domain.Ordens.Models.Ordem;

namespace Uptec.Erp.Api.Controllers.v1.Ordem
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdemController : BaseController
    {
        private readonly IOrdemService _ordemService;
        private readonly IOrdemRepository _ordemRepository;

        public OrdemController(INotificationHandler<DomainNotification> notifications,
                              IOrdemService ordemService,
                              IOrdemRepository ordemRepository,
                              IUnitOfWork uow,
                              IBus bus)
            : base(notifications, uow, bus)
        {
            _ordemService = ordemService;
            _ordemRepository = ordemRepository;
        }

        [HttpGet("GetFullById/{id:guid}")]
        public IActionResult GetFullById(Guid id)
        {
            return Response(_ordemRepository.GetSuperFullById(id).Map<OrdemAlias, OrdemViewModel>());
        }

        //[HttpGet("{pageNumber:int}/{pageSize:int}")]
        //public PagedViewModel<OrdemViewModel> Get(int pageNumber, int pageSize)
        //{
        //    return _ordemRepository.GetPaged(pageNumber, pageSize)
        //        .Map<Paged<OrdemAlias>, PagedViewModel<OrdemViewModel>>();
        //}

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public PagedViewModel<OrdemViewModel> Get(int pageNumber, int pageSize,
                [FromQuery] int status, [FromQuery] string campo, [FromQuery] string search)
        {

            var retorno = _ordemRepository.GetPaged(pageNumber, pageSize, status, campo, search.ReplaceNull());

            return retorno.Map<Paged<OrdemAlias>, PagedViewModel<OrdemViewModel>>();
        }

        [HttpGet("OrdemLotes/{clienteId:guid}")]
        public IActionResult GetOrdemLotesToNfeSaida(Guid clienteId)
        {
            var ret = _ordemRepository.GetOrdemLotesToNfeSaida(clienteId).ToList().Map<List<OrdemLote>, List<OrdemLoteNfeSaidaViewModel>>();
            return Response(ret);
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrdemAddViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            var ToDomainModel = viewModel.Map<OrdemAddViewModel, OrdemAlias>();

            StartTransaction();

            _ordemService.Add(ToDomainModel);

            Commit();

            return Response(ToDomainModel.Id);
        }

        [HttpPut]
        public IActionResult Put([FromBody] OrdemFinalizarViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            var lotes = viewModel.OrdemLotes.Map<List<OrdemLoteFinalizarViewModel>, List<OrdemLote>>();

            StartTransaction();

            _ordemService.Finalizar(viewModel.Id, lotes);

            Commit();

            return Response();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            StartTransaction();

            _ordemService.Delete(id);

            Commit();

            return Response();
        }

        [HttpGet("LinhaProducao")]
        public IActionResult GetLinhaProducao()
        {
            var linha = _ordemRepository.GetLinhaProducao()
                .ToList().Map<List<LinhaProducao>, List<LinhaProducaoViewModel>>();

            return Response(linha);
        }
    }
}
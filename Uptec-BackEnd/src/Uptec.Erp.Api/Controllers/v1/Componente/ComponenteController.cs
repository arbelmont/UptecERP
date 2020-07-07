using ExpressMapper.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.DomainNotifications;
using Definitiva.Shared.Domain.Interfaces;
using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Infra.Support.Helpers;
using Uptec.Erp.Producao.Domain.Componentes.Interfaces;
using Uptec.Erp.Api.ViewModels.Producao.Componente;
using Uptec.Erp.Api.ViewModels.Shared;

using ComponenteAlias = Uptec.Erp.Producao.Domain.Componentes.Models.Componente;
using Uptec.Erp.Producao.Domain.Componentes.Models;
using Uptec.Erp.Shared.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Uptec.Erp.Api.Controllers.v1.Componente
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ComponenteController : BaseController
    {
        private readonly IComponenteService _componenteService;
        private readonly IComponenteRepository _componenteRepository;

        public ComponenteController(INotificationHandler<DomainNotification> notifications,
                              IComponenteService componenteService,
                              IComponenteRepository componenteRepository,
                              IUnitOfWork uow,
                              IBus bus) : base(notifications, uow, bus)
        {
            _componenteService = componenteService;
            _componenteRepository = componenteRepository;
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            return Response(_componenteRepository.GetById(id).Map<ComponenteAlias, ComponenteViewModel>());
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public PagedViewModel<ComponenteViewModel> Get(int pageNumber, int pageSize, [FromQuery] string search)
        {
            return _componenteRepository.GetPaged(pageNumber, pageSize, search.ReplaceNull())
                .Map<Paged<ComponenteAlias>, PagedViewModel<ComponenteViewModel>>();
        }

        [HttpPost]
        public IActionResult Post([FromBody] ComponenteViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            viewModel.Id = Guid.NewGuid();

            _componenteService.Add(viewModel.Map<ComponenteViewModel, ComponenteAlias>());

            Commit();


            return Response(viewModel.Id);
        }

        [HttpPut]
        public IActionResult Put([FromBody] ComponenteViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _componenteService.Update(viewModel.Map<ComponenteViewModel, ComponenteAlias>());

            Commit();

            return Response();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            _componenteService.Delete(id);

            Commit();

            return Response();
        }

        #region Movimentos

        [HttpGet("Movimentos/{id:guid}")]
        public IActionResult GetMovimento(Guid id)
        {
            return Response(_componenteRepository.GetMovimentoById(id).Map<ComponenteMovimento, ComponenteMovimentoViewModel>());
        }

        [HttpGet("Movimentos/{pageNumber:int}/{pageSize:int}")]
        public PagedViewModel<ComponenteMovimentoViewModel> Get(int pageNumber, int pageSize, [FromQuery] Guid componenteId, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            if (startDate == DateTime.MinValue)
                startDate = DateTime.Now.AddMonths(-1);

            if (endDate == DateTime.MinValue)
                endDate = DateTime.Now;

            return _componenteRepository.GetPagedMovimento(componenteId, startDate, endDate, pageNumber, pageSize)
                .Map<Paged<ComponenteMovimento>, PagedViewModel<ComponenteMovimentoViewModel>>();
        }

        [HttpPost("Movimentos")]
        public IActionResult Post([FromBody] ComponenteMovimentoViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            viewModel.Id = Guid.NewGuid();

            if (viewModel.TipoMovimento == TipoMovimentoEstoque.Entrada)
                _componenteService.AddMovimentoEntrada(viewModel.Map<ComponenteMovimentoViewModel, ComponenteMovimento>());
            else
                _componenteService.AddMovimentoSaida(viewModel.Map<ComponenteMovimentoViewModel, ComponenteMovimento>());

            Commit();

            return Response();
        }

        #endregion
    }
}

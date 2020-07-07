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
using Uptec.Erp.Api.ViewModels.Producao.Pecas;
using Uptec.Erp.Api.ViewModels.Shared;
using Uptec.Erp.Producao.Domain.Pecas.Interfaces;
using PecaAlias = Uptec.Erp.Producao.Domain.Pecas.Models.Peca;

namespace Uptec.Erp.Api.Controllers.v1.Peca
{

    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class PecaController : BaseController
    {
        private readonly IPecaService _pecaService;
        private readonly IPecaRepository _pecaRepository;

        public PecaController(INotificationHandler<DomainNotification> notifications,
                              IPecaService pecaService,
                              IPecaRepository pecaRepository,
                              IUnitOfWork uow,
                              IBus bus) : base(notifications, uow, bus)
        {
            _pecaService = pecaService;
            _pecaRepository = pecaRepository;
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            return Response(_pecaRepository.GetById(id).Map<PecaAlias, PecaViewModel>());
        }

        [HttpGet("GetFullById/{id:Guid}")]
        public IActionResult GetByIdWithAggregate(Guid id)
        {
            return Response(_pecaRepository.GetByIdWithAggregate(id).Map<PecaAlias, PecaViewModel>());
        }

        [HttpGet("GetByCodigoFornecedor/{fornecedorId:Guid}/{codigo}")]
        public IActionResult GetByCodigoFornecedor(Guid fornecedorId, string codigo)
        {
            return Response(_pecaRepository.GetByCodigoFornecedor(fornecedorId, codigo).Map<PecaAlias, PecaViewModel>());
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public PagedViewModel<PecaViewModel> Get(int pageNumber, int pageSize, [FromQuery] string search)
        {
            return _pecaRepository.GetPaged(pageNumber, pageSize, search.ReplaceNull())
                .Map<Paged<PecaAlias>, PagedViewModel<PecaViewModel>>();
        }

        [HttpGet("Dropdown")]
        public IActionResult GetPecasTipoPecas()
        {
            var model = _pecaRepository.GetAllTipoPeca().ToList().Map<List<PecaAlias>, List<PecaViewModel>>();

            var dropdownList = new List<DropDownViewModel>();

            model.ForEach(peca =>
            {
                var name = $"{peca.Codigo.PadRight(20, ' ')} - [{peca.Descricao.PadRight(35, ' ')}] ";
                dropdownList.Add(new DropDownViewModel { Value = peca.Id.ToString(), Name = name });
            }
            );

            return Response(dropdownList);
        }

        [HttpGet("GetToProducao")]
        public IActionResult GetToProducao([FromQuery] string search)
        {
            if(search.IsNullOrWhiteSpace())
                return Response();

            var model = _pecaRepository.GetToProducao(search).ToList().Map<List<PecaAlias>, List<PecaViewModel>>();

            return Response(model);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PecaViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            if (viewModel.Componentes == null)
                viewModel.Componentes = new List<PecaComponenteViewModel>();

            var peca = viewModel.Map<PecaViewModel, PecaAlias>();
            _pecaService.Add(peca);

            Commit();

            return Response(peca.Id);
        }

        [HttpPut]
        public IActionResult Put([FromBody] PecaViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _pecaService.Update(viewModel.Map<PecaViewModel, PecaAlias>());

            Commit();

            return Response();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            _pecaService.Delete(id);

            Commit();

            return Response();
        }
    }
}

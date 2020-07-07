using System;
using System.Collections.Generic;
using System.Linq;
using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.DomainNotifications;
using Definitiva.Shared.Domain.Interfaces;
using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Infra.Support.Helpers;
using ExpressMapper.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uptec.Erp.Api.ViewModels.Producao.Transportadoras;
using Uptec.Erp.Api.ViewModels.Shared;
using Uptec.Erp.Producao.Domain.Transportadoras.Interfaces;
using Uptec.Erp.Producao.Domain.Transportadoras.Models;
using TransportadoraAlias = Uptec.Erp.Producao.Domain.Transportadoras.Models.Transportadora;

namespace Uptec.Erp.Api.Controllers.v1.Transportadora
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class TransportadoraController : BaseController
    {
        private readonly ITransportadoraService _transportadoraService;
        private readonly ITransportadoraRepository _transportadoraRepository;
        
        public TransportadoraController(INotificationHandler<DomainNotification> notifications,
                                        ITransportadoraService transportadoraService,
                                        ITransportadoraRepository transportadoraRepository,
                                        IUnitOfWork uow,
                                        IBus bus ) : base(notifications, uow, bus)
        {
            _transportadoraService = transportadoraService;
            _transportadoraRepository = transportadoraRepository;
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            return Response(_transportadoraRepository.GetById(id).Map<TransportadoraAlias, TransportadoraViewModel>());
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public PagedViewModel<TransportadoraViewModel> Get(int pageNumber, int pageSize, [FromQuery] string search)
        {
            return _transportadoraRepository.GetPaged(pageNumber, pageSize, search.ReplaceNull())
                .Map<Paged<TransportadoraAlias>, PagedViewModel<TransportadoraViewModel>>();
        }

        [HttpGet]
        [Route("TipoEntregaPadrao")]
        public IActionResult GetTipoEntregaPadrao()
        {
            return Response(EnumViewModel.TransportadoraTipoEntregaPadrao);
        }

        [HttpPost]
        public IActionResult Post([FromBody] TransportadoraViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            viewModel.Id = Guid.NewGuid();
            viewModel.Endereco.TransportadoraId = viewModel.Id;
            viewModel.Telefone.TransportadoraId = viewModel.Id;
  
            _transportadoraService.Add(viewModel.Map<TransportadoraViewModel, TransportadoraAlias>());

            Commit();

            return Response(viewModel.Id);
        }

        [HttpPut]
        public IActionResult Put([FromBody] TransportadoraViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _transportadoraService.Update(viewModel.Map<TransportadoraViewModel, TransportadoraAlias>());

            Commit();

            return Response();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            _transportadoraService.Delete(id);

            Commit();

            return Response();
        }

        #region Enderecos
        [HttpGet("Endereco/{enderecoId:guid}")]
        public IActionResult GetEndereco(Guid enderecoId)
        {
            return Response(_transportadoraRepository.GetEndereco(enderecoId).Map<TransportadoraEndereco, TransportadoraEnderecoViewModel>());
        }

        [HttpGet("Endereco/Lista/{transportadoraId:guid}")]
        public IActionResult GetEnderecos(Guid transportadoraId)
        {
            var enderecos = _transportadoraRepository.GetEnderecos(transportadoraId).ToList();
            return Response(enderecos.Map<List<TransportadoraEndereco>, List<TransportadoraEnderecoViewModel>>());
        }

        [HttpPut("Endereco")]
        public IActionResult PutEndereco([FromBody] TransportadoraEnderecoViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _transportadoraService.UpdateEndereco(viewModel.Map<TransportadoraEnderecoViewModel, TransportadoraEndereco>());

            Commit();

            return Response();
        }

        [HttpPost("Endereco")]
        public IActionResult PostEndereco([FromBody] TransportadoraEnderecoViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _transportadoraService.AddEndereco(viewModel.Map<TransportadoraEnderecoViewModel, TransportadoraEndereco>());

            Commit();

            return Response();
        }

        [HttpDelete("Endereco/{id:guid}")]
        public IActionResult DeleteEndereco(Guid id)
        {
            _transportadoraService.DeleteEndereco(id);

            Commit();

            return Response();
        }
        #endregion

        #region Telefones
        [HttpGet("Telefone/{telefoneId:guid}")]
        public IActionResult GetTelefone(Guid telefoneId)
        {
            return Response(_transportadoraRepository.GetTelefone(telefoneId).Map<TransportadoraTelefone, TransportadoraTelefoneViewModel>());
        }

        [HttpGet("Telefone/Lista/{transportadoraId:guid}")]
        public IActionResult GetTelefones(Guid transportadoraId)
        {
            var telefones = _transportadoraRepository.GetTelefones(transportadoraId).ToList();
            return Response(telefones.Map<List<TransportadoraTelefone>, List<TransportadoraTelefoneViewModel>>());
        }

        [HttpPut("Telefone")]
        public IActionResult PutTelefone([FromBody] TransportadoraTelefoneViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _transportadoraService.UpdateTelefone(viewModel.Map<TransportadoraTelefoneViewModel, TransportadoraTelefone>());

            Commit();

            return Response();
        }

        [HttpPost("Telefone")]
        public IActionResult PostTelefone([FromBody] TransportadoraTelefoneViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _transportadoraService.AddTelefone(viewModel.Map<TransportadoraTelefoneViewModel, TransportadoraTelefone>());

            Commit();

            return Response();
        }

        [HttpDelete("Telefone/{id:guid}")]
        public IActionResult DeleteTelefone(Guid id)
        {
            _transportadoraService.DeleteTelefone(id);

            Commit();

            return Response();
        }
        #endregion
    }
}

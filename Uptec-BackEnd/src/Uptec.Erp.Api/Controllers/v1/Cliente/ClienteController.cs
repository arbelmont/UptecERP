using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Uptec.Erp.Api.ViewModels.Producao.Clientes;
using Uptec.Erp.Api.ViewModels.Shared;
using Uptec.Erp.Producao.Domain.Clientes.Interfaces;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using ClienteAlias = Uptec.Erp.Producao.Domain.Clientes.Models.Cliente;

namespace Uptec.Erp.Api.Controllers.v1.Cliente
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(Policy = "Cadastro")]
    public class ClienteController : BaseController
    {
        private readonly IClienteService _clienteService;
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(INotificationHandler<DomainNotification> notifications,
                                        IClienteService clienteService,
                                        IClienteRepository clienteRepository,
                                        IUnitOfWork uow,
                                        IBus bus) : base(notifications, uow, bus)
        {
            _clienteService = clienteService;
            _clienteRepository = clienteRepository;
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            return Response(_clienteRepository.GetById(id).Map<ClienteAlias, ClienteViewModel>());
        }

        [HttpGet()]
        public IActionResult GetToNfeSaida([FromQuery] string search)
        {
            return Response(_clienteRepository.GetToNfeSaida(search).Map<IEnumerable<ClienteAlias>, IEnumerable<ClienteViewModel>>());
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public PagedViewModel<ClienteViewModel> Get(int pageNumber, int pageSize, [FromQuery] string search)
        {
            return _clienteRepository.GetPaged(pageNumber, pageSize, search.ReplaceNull())
                .Map<Paged<ClienteAlias>, PagedViewModel<ClienteViewModel>>();
        }

        [HttpPost]
        public IActionResult Post([FromBody] ClienteViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            viewModel.Id = Guid.NewGuid();
            viewModel.Endereco.ClienteId = viewModel.Id;
            viewModel.Telefone.ClienteId = viewModel.Id;

            _clienteService.Add(viewModel.Map<ClienteViewModel, ClienteAlias>());

            Commit();


            return Response(viewModel.Id);
        }

        [HttpPut]
        public IActionResult Put([FromBody] ClienteViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _clienteService.Update(viewModel.Map<ClienteViewModel, ClienteAlias>());

            Commit();

            return Response();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            _clienteService.Delete(id);

            Commit();

            return Response();
        }

        #region Enderecos
        [HttpGet("Endereco/{enderecoId:guid}")]
        public IActionResult GetEndereco(Guid enderecoId)
        {
            return Response(_clienteRepository.GetEndereco(enderecoId).Map<ClienteEndereco, ClienteEnderecoViewModel>());
        }

        [HttpGet("Endereco/Lista/{clienteId:guid}")]
        public IActionResult GetEnderecos(Guid clienteId)
        {
            var enderecos = _clienteRepository.GetEnderecos(clienteId);
            return Response(enderecos.Map<IEnumerable<ClienteEndereco>, IEnumerable<ClienteEnderecoViewModel>>());
        }

        [HttpPut("Endereco")]
        public IActionResult PutEndereco([FromBody] ClienteEnderecoViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _clienteService.UpdateEndereco(viewModel.Map<ClienteEnderecoViewModel, ClienteEndereco>());

            Commit();

            return Response();
        }

        [HttpPost("Endereco")]
        public IActionResult PostEndereco([FromBody] ClienteEnderecoViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _clienteService.AddEndereco(viewModel.Map<ClienteEnderecoViewModel, ClienteEndereco>());

            Commit();

            return Response();
        }

        [HttpDelete("Endereco/{id:guid}")]
        public IActionResult DeleteEndereco(Guid id)
        {
            _clienteService.DeleteEndereco(id);

            Commit();

            return Response();
        }
        #endregion

        #region Telefones
        [HttpGet("Telefone/{telefoneId:guid}")]
        public IActionResult GetTelefone(Guid telefoneId)
        {
            return Response(_clienteRepository.GetTelefone(telefoneId).Map<ClienteTelefone, ClienteTelefoneViewModel>());
        }

        [HttpGet("Telefone/Lista/{clienteId:guid}")]
        public IActionResult GetTelefones(Guid clienteId)
        {
            var telefones = _clienteRepository.GetTelefones(clienteId);
            return Response(telefones.Map<IEnumerable<ClienteTelefone>, IEnumerable<ClienteTelefoneViewModel>>());
        }

        [HttpPut("Telefone")]
        public IActionResult PutTelefone([FromBody] ClienteTelefoneViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _clienteService.UpdateTelefone(viewModel.Map<ClienteTelefoneViewModel, ClienteTelefone>());

            Commit();

            return Response();
        }

        [HttpPost("Telefone")]
        public IActionResult PostTelefone([FromBody] ClienteTelefoneViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _clienteService.AddTelefone(viewModel.Map<ClienteTelefoneViewModel, ClienteTelefone>());

            Commit();

            return Response();
        }

        [HttpDelete("Telefone/{id:guid}")]
        public IActionResult DeleteTelefone(Guid id)
        {
            _clienteService.DeleteTelefone(id);

            Commit();

            return Response();
        }
        #endregion
    }
}

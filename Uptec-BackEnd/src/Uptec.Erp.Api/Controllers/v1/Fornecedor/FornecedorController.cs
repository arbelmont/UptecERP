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
using Uptec.Erp.Api.ViewModels.Producao.Fornecedores;
using Uptec.Erp.Api.ViewModels.Shared;
using Uptec.Erp.Producao.Domain.Fornecedores.Interfaces;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using FornecedorAlias = Uptec.Erp.Producao.Domain.Fornecedores.Models.Fornecedor;

namespace Uptec.Erp.Api.Controllers.v1.Fornecedor
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class FornecedorController : BaseController
    {
        private readonly IFornecedorService _fornecedorService;
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorController(INotificationHandler<DomainNotification> notifications,
                                        IFornecedorService fornecedorService,
                                        IFornecedorRepository fornecedorRepository,
                                        IUnitOfWork uow,
                                        IBus bus) : base(notifications, uow, bus)
        {
            _fornecedorService = fornecedorService;
            _fornecedorRepository = fornecedorRepository;
        }

        [HttpGet("{id:guid}")]
        public IActionResult Get(Guid id)
        {
            return Response(_fornecedorRepository.GetById(id).Map<FornecedorAlias, FornecedorViewModel>());
        }

        [HttpGet()]
        public IActionResult GetToNfeSaida([FromQuery] string search)
        {
            return Response(_fornecedorRepository.GetToNfeSaida(search).ToList().Map<List<FornecedorAlias>, List<FornecedorViewModel>>());
        }

        [HttpGet("{pageNumber:int}/{pageSize:int}")]
        public PagedViewModel<FornecedorViewModel> Get(int pageNumber, int pageSize, [FromQuery] string search)
        {
            return _fornecedorRepository.GetPaged(pageNumber, pageSize, search.ReplaceNull())
                .Map<Paged<FornecedorAlias>, PagedViewModel<FornecedorViewModel>>();
        }

        [HttpPost]
        public IActionResult Post([FromBody] FornecedorViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            viewModel.Id = Guid.NewGuid();
            viewModel.Endereco.FornecedorId = viewModel.Id;
            viewModel.Telefone.FornecedorId = viewModel.Id;

            _fornecedorService.Add(viewModel.Map<FornecedorViewModel, FornecedorAlias>());

            Commit();


            return Response(viewModel.Id);
        }

        [HttpPut]
        public IActionResult Put([FromBody] FornecedorViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _fornecedorService.Update(viewModel.Map<FornecedorViewModel, FornecedorAlias>());

            Commit();

            return Response();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            _fornecedorService.Delete(id);

            Commit();

            return Response();
        }

        #region Enderecos
        [HttpGet("Endereco/{enderecoId:guid}")]
        public IActionResult GetEndereco(Guid enderecoId)
        {
            return Response(_fornecedorRepository.GetEndereco(enderecoId).Map<FornecedorEndereco, FornecedorEnderecoViewModel>());
        }

        [HttpGet("Endereco/Lista/{fornecedorId:guid}")]
        public IActionResult GetEnderecos(Guid fornecedorId)
        {
            var enderecos = _fornecedorRepository.GetEnderecos(fornecedorId).ToList();
            return Response(enderecos.Map<List<FornecedorEndereco>, List<FornecedorEnderecoViewModel>>());
        }

        [HttpPut("Endereco")]
        public IActionResult PutEndereco([FromBody] FornecedorEnderecoViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _fornecedorService.UpdateEndereco(viewModel.Map<FornecedorEnderecoViewModel, FornecedorEndereco>());

            Commit();

            return Response();
        }

        [HttpPost("Endereco")]
        public IActionResult PostEndereco([FromBody] FornecedorEnderecoViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _fornecedorService.AddEndereco(viewModel.Map<FornecedorEnderecoViewModel, FornecedorEndereco>());

            Commit();

            return Response();
        }

        [HttpDelete("Endereco/{id:guid}")]
        public IActionResult DeleteEndereco(Guid id)
        {
            _fornecedorService.DeleteEndereco(id);

            Commit();

            return Response();
        }
        #endregion

        #region Telefones
        [HttpGet("Telefone/{telefoneId:guid}")]
        public IActionResult GetTelefone(Guid telefoneId)
        {
            return Response(_fornecedorRepository.GetTelefone(telefoneId).Map<FornecedorTelefone, FornecedorTelefoneViewModel>());
        }

        [HttpGet("Telefone/Lista/{fornecedorId:guid}")]
        public IActionResult GetTelefones(Guid fornecedorId)
        {
            var telefones = _fornecedorRepository.GetTelefones(fornecedorId).ToList();
            return Response(telefones.Map<List<FornecedorTelefone>, List<FornecedorTelefoneViewModel>>());
        }

        [HttpPut("Telefone")]
        public IActionResult PutTelefone([FromBody] FornecedorTelefoneViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _fornecedorService.UpdateTelefone(viewModel.Map<FornecedorTelefoneViewModel, FornecedorTelefone>());

            Commit();

            return Response();
        }

        [HttpPost("Telefone")]
        public IActionResult PostTelefone([FromBody] FornecedorTelefoneViewModel viewModel)
        {
            if (!IsValidModelState()) return Response();

            _fornecedorService.AddTelefone(viewModel.Map<FornecedorTelefoneViewModel, FornecedorTelefone>());

            Commit();

            return Response();
        }

        [HttpDelete("Telefone/{id:guid}")]
        public IActionResult DeleteTelefone(Guid id)
        {
            _fornecedorService.DeleteTelefone(id);

            Commit();

            return Response();
        }
        #endregion
    }
}

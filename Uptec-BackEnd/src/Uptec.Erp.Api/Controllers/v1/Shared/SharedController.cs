using System.Collections.Generic;
using System.Linq;
using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.DomainNotifications;
using Definitiva.Shared.Domain.Interfaces;
using ExpressMapper.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Uptec.Erp.Api.ViewModels.Producao.Pecas;
using Uptec.Erp.Api.ViewModels.Shared;
using Uptec.Erp.Producao.Domain.Clientes.Interfaces;
using Uptec.Erp.Producao.Domain.Componentes.Interfaces;
using Uptec.Erp.Producao.Domain.Pecas.Interfaces;
using Uptec.Erp.Producao.Domain.Transportadoras.Interfaces;
using Uptec.Erp.Shared.Domain.ValueObjects;
using Uptec.Erp.Producao.Domain.Pecas.Models;
using Uptec.Erp.Producao.Domain.Fornecedores.Interfaces;

namespace Uptec.Erp.Api.Controllers.v1.Shared
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class SharedController : BaseController
    {
        private readonly ITransportadoraRepository _transportadoraRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IPecaRepository _pecaRepository;
        private readonly IComponenteRepository _componenteRepository;

        public SharedController(INotificationHandler<DomainNotification> notifications,
                                IUnitOfWork uow,
                                IBus bus,
                                ITransportadoraRepository transportadoraRepository,
                                IClienteRepository clienteRepository,
                                IFornecedorRepository fornecedorRepository,
                                IPecaRepository pecaRepository,
                                IComponenteRepository componenteRepository) 
            : base(notifications, uow, bus)
        {
            _transportadoraRepository = transportadoraRepository;
            _clienteRepository = clienteRepository;
            _fornecedorRepository = fornecedorRepository;
            _pecaRepository = pecaRepository;
            _componenteRepository = componenteRepository;
        }

        [HttpGet]
        [Route("Estados")]
        public IActionResult GetEstados()
        {
            return Response(Estado.Listagem().Map<IEnumerable<Estado>, IEnumerable<EstadoViewModel>>());
        }

        [HttpGet]
        [Route("Cidades/{uf}")]
        public IActionResult GetCidades(string uf)
        {
            return Response(Cidade.GetCidades(uf).Map<IEnumerable<Cidade>, IEnumerable<CidadeViewModel>>());
        }

        [HttpGet]
        [Route("TelefoneTipos")]
        public IActionResult GetTiposTelefone()
        {
            return Response(EnumViewModel.TelefoneTipo);
        }

        [HttpGet]
        [Route("EnderecoTipos")]
        public IActionResult GetTiposEndereco()
        {
            return Response(EnumViewModel.EnderecoTipo);
        }

        [HttpGet]
        [Route("UnidadesMedida")]
        public IActionResult GetUnidadesMedida()
        {
            return Response(EnumViewModel.UnidadeMedida);
        }

        [HttpGet("Transportadoras")]
        public IActionResult GetTransportadoras()
        {
            var dropdownList = new List<DropDownViewModel>();
            var transportadoras = _transportadoraRepository.GetAll();

            transportadoras.OrderBy(c => c.NomeFantasia).ToList()
                .ForEach(transportadora => dropdownList.Add(
                    new DropDownViewModel { Value = transportadora.Id.ToString(), Name = transportadora.NomeFantasia }
                    ));

            return Response(dropdownList);
        }

        [HttpGet("Clientes")]
        public IActionResult GetClientes()
        {
            var dropdownList = new List<DropDownViewModel>();
            var clientes = _clienteRepository.GetAll();

            clientes.OrderBy(c => c.NomeFantasia).ToList()
                .ForEach(cliente => dropdownList.Add(
                    new DropDownViewModel { Value = cliente.Id.ToString(), Name = cliente.NomeFantasia }
                    ));

            return Response(dropdownList);
        }

        [HttpGet("Fornecedores")]
        public IActionResult GetFornecedores()
        {
            var dropdownList = new List<DropDownViewModel>();
            var fornecedores = _fornecedorRepository.GetAll();

            fornecedores.OrderBy(c => c.NomeFantasia).ToList()
                .ForEach(fornecedor => dropdownList.Add(
                    new DropDownViewModel { Value = fornecedor.Id.ToString(), Name = fornecedor.NomeFantasia }
                    ));

            return Response(dropdownList);
        }

        [HttpGet("Componentes")]
        public IActionResult GetComponentes()
        {
            var dropdownList = new List<DropDownViewModel>();
            var componentes = _componenteRepository.GetAll();

            componentes.ToList()
                .ForEach(componente => dropdownList.Add(
                    new DropDownViewModel { Value = componente.Id.ToString(), Name = componente.Descricao }
                    ));

            return Response(dropdownList);
        }

        [HttpGet("TipoEmissor")]
        public IActionResult GetTipoEmissor()
        {
            return Response(EnumViewModel.TipoEmissor);
        }

        [HttpGet("StatusNfEntrada")]
        public IActionResult GetStatusNfEntrada()
        {
            return Response(EnumViewModel.StatusNfEntrada);
        }

    }
}
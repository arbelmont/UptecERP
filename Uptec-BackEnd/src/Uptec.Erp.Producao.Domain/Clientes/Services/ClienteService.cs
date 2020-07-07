using System;
using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.Services;
using Uptec.Erp.Producao.Domain.Clientes.Interfaces;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Domain.Clientes.Validations;

namespace Uptec.Erp.Producao.Domain.Clientes.Services
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IBus bus, IClienteRepository clienteRepository) : base(bus)
        {
            _clienteRepository = clienteRepository;
        }

        #region cliente
        public void Add(Cliente cliente)
        {
            if(!ValidateEntity(cliente)) return;

            if(!ValidateBusinessRules(new ClienteCanAddValidation(_clienteRepository).Validate(cliente))) return;

            cliente.Enderecos.Add(cliente.Endereco);
            cliente.Telefones.Add(cliente.Telefone);

            _clienteRepository.Add(cliente);
        }

        public void Update(Cliente cliente)
        {
            if (!ValidateEntity(cliente)) return;

            if (!ValidateBusinessRules(new ClienteCanUpdateValidation(_clienteRepository).Validate(cliente))) return;

            _clienteRepository.Update(cliente);
        }

        public void Delete(Guid id)
        {
            var Cliente = _clienteRepository.GetById(id);

            if (Cliente == null)
            {
                NotifyError("Cliente inexistente.");
                return;
            }

            Cliente.Delete();
            _clienteRepository.Delete(Cliente);
        }
        #endregion

        #region ClienteEndereço
        public void AddEndereco(ClienteEndereco cliente)
        {
            if (!cliente.IsValid())
            {
                NotifyDomainValidationErrors(cliente.Validation.Result);
                return;
            }

            _clienteRepository.AddEndereco(cliente);
        }

        public void UpdateEndereco(ClienteEndereco cliente)
        {
            if (!cliente.IsValid())
            {
                NotifyDomainValidationErrors(cliente.Validation.Result);
                return;
            }

            _clienteRepository.UpdateEndereco(cliente);
        }

        public void DeleteEndereco(Guid id)
        {
            var cliente = _clienteRepository.GetEndereco(id);

            if (cliente == null)
            {
                NotifyError("Endereço inexistente.");
                return;
            }

            cliente.Delete();
            _clienteRepository.DeleteEndereco(cliente);
        }
        #endregion

        #region Telefone
        public void AddTelefone(ClienteTelefone telefone)
        {
            if (!telefone.IsValid())
            {
                NotifyDomainValidationErrors(telefone.Validation.Result);
                return;
            }

            _clienteRepository.AddTelefone(telefone);
        }

        public void UpdateTelefone(ClienteTelefone telefone)
        {
            if (!telefone.IsValid())
            {
                NotifyDomainValidationErrors(telefone.Validation.Result);
                return;
            }

            _clienteRepository.UpdateTelefone(telefone);
        }

        public void DeleteTelefone(Guid id)
        {
            var telefone = _clienteRepository.GetTelefone(id);

            if (telefone == null)
            {
                NotifyError("Telefone/Contato inexistente.");
                return;
            }

            telefone.Delete();
            _clienteRepository.DeleteTelefone(telefone);
        }
        #endregion

        public void Dispose()
        {
            _clienteRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
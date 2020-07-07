using System;
using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.Services;
using Uptec.Erp.Producao.Domain.Transportadoras.Interfaces;
using Uptec.Erp.Producao.Domain.Transportadoras.Models;
using Uptec.Erp.Producao.Domain.Transportadoras.Validations;

namespace Uptec.Erp.Producao.Domain.Transportadoras.Services
{
    public class TransportadoraService : BaseService, ITransportadoraService
    {
        private readonly ITransportadoraRepository _transportadoraRepository;

        public TransportadoraService(IBus bus, 
                                     ITransportadoraRepository transportadoraRepository) : base(bus)
        {
            _transportadoraRepository = transportadoraRepository;
        }

        #region Transportadora
        public void Add(Transportadora transportadora)
        {
            if(!ValidateEntity(transportadora)) return;

            if(!ValidateBusinessRules(new TransportadoraCanAddValidation(_transportadoraRepository).Validate(transportadora))) return;

            transportadora.Enderecos.Add(transportadora.Endereco);
            transportadora.Telefones.Add(transportadora.Telefone);

            _transportadoraRepository.Add(transportadora);
        }

        public void Update(Transportadora transportadora)
        {
            if (!ValidateEntity(transportadora)) return;

            if (!ValidateBusinessRules(new TransportadoraCanUpdateValidation(_transportadoraRepository).Validate(transportadora))) return;

            _transportadoraRepository.Update(transportadora);
        }

        public void Delete(Guid id)
        {
            var transportadora = _transportadoraRepository.GetById(id);

            if (transportadora == null)
            {
                NotifyError("Transportadora inexistente.");
                return;
            }

            transportadora.Delete();
            _transportadoraRepository.Delete(transportadora);
        }
        #endregion

        #region TransportadoraEndereco
        public void AddEndereco(TransportadoraEndereco endereco)
        {
            if (!endereco.IsValid())
            {
                NotifyDomainValidationErrors(endereco.Validation.Result);
                return;
            }

            _transportadoraRepository.AddEndereco(endereco);
        }

        public void UpdateEndereco(TransportadoraEndereco endereco)
        {
            if (!endereco.IsValid())
            {
                NotifyDomainValidationErrors(endereco.Validation.Result);
                return;
            }

            _transportadoraRepository.UpdateEndereco(endereco);
        }

        public void DeleteEndereco(Guid id)
        {
            var endereco = _transportadoraRepository.GetEndereco(id);

            if (endereco == null)
            {
                NotifyError("Endereço inexistente.");
                return;
            }

            endereco.Delete();
            _transportadoraRepository.DeleteEndereco(endereco);
        }
        #endregion

        #region TransportadoraTelefone
        public void AddTelefone(TransportadoraTelefone telefone)
        {
            if (!telefone.IsValid())
            {
                NotifyDomainValidationErrors(telefone.Validation.Result);
                return;
            }

            _transportadoraRepository.AddTelefone(telefone);
        }

        public void UpdateTelefone(TransportadoraTelefone telefone)
        {
            if (!telefone.IsValid())
            {
                NotifyDomainValidationErrors(telefone.Validation.Result);
                return;
            }

            _transportadoraRepository.UpdateTelefone(telefone);
        }

        public void DeleteTelefone(Guid id)
        {
            var telefone = _transportadoraRepository.GetTelefone(id);

            if (telefone == null)
            {
                NotifyError("Telefone/Contato inexistente.");
                return;
            }

            telefone.Delete();
            _transportadoraRepository.DeleteTelefone(telefone);
        }
        #endregion

        public void Dispose()
        {
            _transportadoraRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
using System;
using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.Services;
using Uptec.Erp.Producao.Domain.Fornecedores.Interfaces;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Producao.Domain.Fornecedores.Validations;

namespace Uptec.Erp.Producao.Domain.Fornecedores.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorService(IBus bus, IFornecedorRepository fornecedorRepository) : base(bus)
        {
            _fornecedorRepository = fornecedorRepository;
        }

        #region fornecedor
        public void Add(Fornecedor fornecedor)
        {
            if(!ValidateEntity(fornecedor)) return;

            if(!ValidateBusinessRules(new FornecedorCanAddValidation(_fornecedorRepository).Validate(fornecedor))) return;

            fornecedor.Enderecos.Add(fornecedor.Endereco);
            fornecedor.Telefones.Add(fornecedor.Telefone);

            _fornecedorRepository.Add(fornecedor);
        }

        public void Update(Fornecedor fornecedor)
        {
            if (!ValidateEntity(fornecedor)) return;

            if (!ValidateBusinessRules(new FornecedorCanUpdateValidation(_fornecedorRepository).Validate(fornecedor))) return;

            _fornecedorRepository.Update(fornecedor);
        }

        public void Delete(Guid id)
        {
            var Fornecedor = _fornecedorRepository.GetById(id);

            if (Fornecedor == null)
            {
                NotifyError("Fornecedor inexistente.");
                return;
            }

            Fornecedor.Delete();
            _fornecedorRepository.Delete(Fornecedor);
        }
        #endregion

        #region FornecedorEndereço
        public void AddEndereco(FornecedorEndereco fornecedor)
        {
            if (!fornecedor.IsValid())
            {
                NotifyDomainValidationErrors(fornecedor.Validation.Result);
                return;
            }

            _fornecedorRepository.AddEndereco(fornecedor);
        }

        public void UpdateEndereco(FornecedorEndereco fornecedor)
        {
            if (!fornecedor.IsValid())
            {
                NotifyDomainValidationErrors(fornecedor.Validation.Result);
                return;
            }

            _fornecedorRepository.UpdateEndereco(fornecedor);
        }

        public void DeleteEndereco(Guid id)
        {
            var fornecedor = _fornecedorRepository.GetEndereco(id);

            if (fornecedor == null)
            {
                NotifyError("Endereço inexistente.");
                return;
            }

            fornecedor.Delete();
            _fornecedorRepository.DeleteEndereco(fornecedor);
        }
        #endregion

        #region Telefone
        public void AddTelefone(FornecedorTelefone telefone)
        {
            if (!telefone.IsValid())
            {
                NotifyDomainValidationErrors(telefone.Validation.Result);
                return;
            }

            _fornecedorRepository.AddTelefone(telefone);
        }

        public void UpdateTelefone(FornecedorTelefone telefone)
        {
            if (!telefone.IsValid())
            {
                NotifyDomainValidationErrors(telefone.Validation.Result);
                return;
            }

            _fornecedorRepository.UpdateTelefone(telefone);
        }

        public void DeleteTelefone(Guid id)
        {
            var telefone = _fornecedorRepository.GetTelefone(id);

            if (telefone == null)
            {
                NotifyError("Telefone/Contato inexistente.");
                return;
            }

            telefone.Delete();
            _fornecedorRepository.DeleteTelefone(telefone);
        }
        #endregion

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
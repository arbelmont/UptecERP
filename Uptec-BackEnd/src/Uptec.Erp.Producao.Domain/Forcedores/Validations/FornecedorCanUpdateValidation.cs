using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Fornecedores.Interfaces;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Producao.Domain.Fornecedores.Specifications;

namespace Uptec.Erp.Producao.Domain.Fornecedores.Validations
{
    public class FornecedorCanUpdateValidation : DomainValidator<Fornecedor>
    {
        public FornecedorCanUpdateValidation(IFornecedorRepository fornecedorRepository)
        {
            Add(new FornecedorCnpjUnicoSpec(fornecedorRepository, DomainOperation.Update), "Cnpj já cadastrado.");
        }
    }
}
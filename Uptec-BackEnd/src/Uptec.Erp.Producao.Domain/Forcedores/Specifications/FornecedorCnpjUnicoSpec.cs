using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Fornecedores.Interfaces;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;

namespace Uptec.Erp.Producao.Domain.Fornecedores.Specifications
{
    public class FornecedorCnpjUnicoSpec : IDomainSpecification<Fornecedor>
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly DomainOperation _domainOperation;

        public FornecedorCnpjUnicoSpec(IFornecedorRepository fornecedorRepository, DomainOperation domainOperation)
        {
            _fornecedorRepository = fornecedorRepository;
            _domainOperation = domainOperation;
        }

        public bool IsSatisfiedBy(Fornecedor entity)
        {
            var fornecedor = _fornecedorRepository.GetByCnpj(entity.Cnpj.Numero);

            if (_domainOperation == DomainOperation.Add)
                return fornecedor == null;

            return fornecedor == null || fornecedor.Id == entity.Id;
        }
    }
}

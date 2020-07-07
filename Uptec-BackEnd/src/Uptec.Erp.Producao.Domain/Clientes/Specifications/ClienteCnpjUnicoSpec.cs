using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Clientes.Interfaces;
using Uptec.Erp.Producao.Domain.Clientes.Models;

namespace Uptec.Erp.Producao.Domain.Clientes.Specifications
{
    public class ClienteCnpjUnicoSpec : IDomainSpecification<Cliente>
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly DomainOperation _domainOperation;

        public ClienteCnpjUnicoSpec(IClienteRepository clienteRepository, DomainOperation domainOperation)
        {
            _clienteRepository = clienteRepository;
            _domainOperation = domainOperation;
        }

        public bool IsSatisfiedBy(Cliente entity)
        {
            var cliente = _clienteRepository.GetByCnpj(entity.Cnpj.Numero);

            if (_domainOperation == DomainOperation.Add)
                return cliente == null;

            return cliente == null || cliente.Id == entity.Id;
        }
    }
}

using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Clientes.Interfaces;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Domain.Clientes.Specifications;

namespace Uptec.Erp.Producao.Domain.Clientes.Validations
{
    public class ClienteCanAddValidation : DomainValidator<Cliente>
    {
        public ClienteCanAddValidation(IClienteRepository clienteRepository)
        {
            Add(new ClienteCnpjUnicoSpec(clienteRepository, DomainOperation.Add), "Cnpj já cadastrado.");
        }
    }
}
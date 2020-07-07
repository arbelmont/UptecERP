using Definitiva.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Shared.Domain.Interfaces;

namespace Uptec.Erp.Producao.Domain.Clientes.Interfaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Cliente GetByIdWithAggregate(Guid id);
        Cliente GetByIdWithEnderecos(Guid id);
        Cliente GetByCnpj(string cnpj);
        Paged<Cliente> GetPaged(int pageNumber, int pageSize, string search);
        IEnumerable<Cliente> GetToNfeSaida(string search);
        ClienteEndereco GetEndereco(Guid enderecoId);
        IEnumerable<ClienteEndereco> GetEnderecos(Guid clienteId);
        ClienteEndereco AddEndereco(ClienteEndereco endereco);
        ClienteEndereco UpdateEndereco(ClienteEndereco endereco);
        ClienteEndereco DeleteEndereco(ClienteEndereco endereco);

        ClienteTelefone GetTelefone(Guid telefoneId);
        IEnumerable<ClienteTelefone> GetTelefones(Guid clienteId);
        ClienteTelefone AddTelefone(ClienteTelefone telefone);
        ClienteTelefone UpdateTelefone(ClienteTelefone telefone);
        ClienteTelefone DeleteTelefone(ClienteTelefone telefone);
    }
}
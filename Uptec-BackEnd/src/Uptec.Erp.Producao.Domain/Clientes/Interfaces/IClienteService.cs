using System;
using Uptec.Erp.Producao.Domain.Clientes.Models;

namespace Uptec.Erp.Producao.Domain.Clientes.Interfaces
{
    public interface IClienteService : IDisposable
    {
        void Add(Cliente cliente);
        void Update(Cliente cliente);
        void Delete(Guid id);

        void AddEndereco(ClienteEndereco endereco);
        void UpdateEndereco(ClienteEndereco endereco);
        void DeleteEndereco(Guid id);

        void AddTelefone(ClienteTelefone telefone);
        void UpdateTelefone(ClienteTelefone telefone);
        void DeleteTelefone(Guid id);
    }
}
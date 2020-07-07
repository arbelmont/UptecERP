using System;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;

namespace Uptec.Erp.Producao.Domain.Fornecedores.Interfaces
{
    public interface IFornecedorService : IDisposable
    {
        void Add(Fornecedor fornecedor);
        void Update(Fornecedor fornecedor);
        void Delete(Guid id);

        void AddEndereco(FornecedorEndereco endereco);
        void UpdateEndereco(FornecedorEndereco endereco);
        void DeleteEndereco(Guid id);

        void AddTelefone(FornecedorTelefone telefone);
        void UpdateTelefone(FornecedorTelefone telefone);
        void DeleteTelefone(Guid id);
    }
}
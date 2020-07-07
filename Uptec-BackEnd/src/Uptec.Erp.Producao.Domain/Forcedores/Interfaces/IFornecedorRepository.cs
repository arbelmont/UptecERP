using Definitiva.Shared.Domain.Models;
using System;
using System.Collections.Generic;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Shared.Domain.Interfaces;

namespace Uptec.Erp.Producao.Domain.Fornecedores.Interfaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Fornecedor GetByIdWithAggregate(Guid id);
        Fornecedor GetByIdWithEnderecos(Guid id);
        Fornecedor GetByCnpj(string cnpj);
        Paged<Fornecedor> GetPaged(int pageNumber, int pageSize, string search);
        IEnumerable<Fornecedor> GetToNfeSaida(string search);

        FornecedorEndereco GetEndereco(Guid enderecoId);
        IEnumerable<FornecedorEndereco> GetEnderecos(Guid fornecedorId);
        FornecedorEndereco AddEndereco(FornecedorEndereco endereco);
        FornecedorEndereco UpdateEndereco(FornecedorEndereco endereco);
        FornecedorEndereco DeleteEndereco(FornecedorEndereco endereco);

        FornecedorTelefone GetTelefone(Guid telefoneId);
        IEnumerable<FornecedorTelefone> GetTelefones(Guid fornecedorId);
        FornecedorTelefone AddTelefone(FornecedorTelefone telefone);
        FornecedorTelefone UpdateTelefone(FornecedorTelefone telefone);
        FornecedorTelefone DeleteTelefone(FornecedorTelefone telefone);
    }
}
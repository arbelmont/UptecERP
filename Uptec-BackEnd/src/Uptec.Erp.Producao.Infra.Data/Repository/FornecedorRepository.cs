using Definitiva.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Definitiva.Shared.Infra.Support.Helpers;
using Uptec.Erp.Producao.Domain.Fornecedores.Interfaces;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Producao.Infra.Data.Context;

namespace Uptec.Erp.Producao.Infra.Data.Repository
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(ProducaoContext context) : base(context) { }

        #region Fornecedor
        public Fornecedor GetByIdWithAggregate(Guid id)
        {
            return Dbset.AsNoTracking().Include(t => t.Endereco).AsNoTracking().Include(t => t.Telefone).AsNoTracking()
                .FirstOrDefault(t => t.Id == id);
        }

        public Fornecedor GetByIdWithEnderecos(Guid id)
        {
            return Dbset.AsNoTracking().Include(t => t.Enderecos).AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public Fornecedor GetByCnpj(string cnpj)
        {
            return Dbset.AsNoTracking().FirstOrDefault(t => t.Cnpj.Numero == cnpj);
        }

        public Paged<Fornecedor> GetPaged(int pageNumber, int pageSize, string search)
        {
            var skip = (pageNumber - 1) * pageSize;

            var paged = new Paged<Fornecedor>
            {
                List = Dbset.AsNoTracking()
                    .Where(t => t.Cnpj.Numero.Contains(search) || t.RazaoSocial.Contains(search) || t.NomeFantasia.Contains(search))
                    .OrderBy(t => t.NomeFantasia).Skip(skip).Take(pageSize),
                Total = Dbset.AsNoTracking()
                    .Count(t => t.Cnpj.Numero.Contains(search) || t.RazaoSocial.Contains(search) || t.NomeFantasia.Contains(search))
            };

            return paged;
        }

        public IEnumerable<Fornecedor> GetToNfeSaida(string search)
        {
            return Dbset.AsNoTracking().Where(c => c.Cnpj.Numero.Contains(search.ReplaceNull("")) ||
                c.NomeFantasia.Contains(search.ReplaceNull(""))).Include(c => c.Enderecos).AsNoTracking().OrderBy(c => c.NomeFantasia);
        }
        #endregion

        #region FornecedorEndereço
        public FornecedorEndereco GetEndereco(Guid fornecedorId)
        {
            return Db.FornecedorEnderecos.AsNoTracking().FirstOrDefault(t => t.Id == fornecedorId);
        }

        public IEnumerable<FornecedorEndereco> GetEnderecos(Guid fornecedorId)
        {
            return Db.FornecedorEnderecos.AsNoTracking().Where(c => c.FornecedorId == fornecedorId);
        }

        public FornecedorEndereco AddEndereco(FornecedorEndereco endereco)
        {
            return Db.FornecedorEnderecos.Add(endereco).Entity;
        }

        public FornecedorEndereco UpdateEndereco(FornecedorEndereco endereco)
        {
            return Db.FornecedorEnderecos.Update(endereco).Entity;
        }

        public FornecedorEndereco DeleteEndereco(FornecedorEndereco endereco)
        {
            endereco.Delete();
            return Db.FornecedorEnderecos.Update(endereco).Entity;
        }
        #endregion

        #region FornecedorTelefone
        public FornecedorTelefone GetTelefone(Guid telefoneId)
        {
            return Db.FornecedorTelefones.FirstOrDefault(t => t.Id == telefoneId);
        }

        public IEnumerable<FornecedorTelefone> GetTelefones(Guid fornecedorId)
        {
            return Db.FornecedorTelefones.Where(t => t.FornecedorId == fornecedorId);
        }

        public FornecedorTelefone AddTelefone(FornecedorTelefone telefone)
        {
            return Db.FornecedorTelefones.Add(telefone).Entity;
        }

        public FornecedorTelefone UpdateTelefone(FornecedorTelefone telefone)
        {
            return Db.FornecedorTelefones.Update(telefone).Entity;
        }

        public FornecedorTelefone DeleteTelefone(FornecedorTelefone telefone)
        {
            telefone.Delete();
            return Db.FornecedorTelefones.Update(telefone).Entity;
        }
        #endregion
    }
}

using Definitiva.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Definitiva.Shared.Infra.Support.Helpers;
using Uptec.Erp.Producao.Domain.Clientes.Interfaces;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Infra.Data.Context;

namespace Uptec.Erp.Producao.Infra.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(ProducaoContext context) : base(context) { }

        #region Cliente
        public Cliente GetByIdWithAggregate(Guid id)
        {
            return Dbset.AsNoTracking().Include(t => t.Enderecos).Include(t => t.Telefone)
                .FirstOrDefault(t => t.Id == id);
        }
        public Cliente GetByIdWithEnderecos(Guid id)
        {
            return Dbset.AsNoTracking().Include(t => t.Enderecos).AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public Cliente GetByCnpj(string cnpj)
        {
            return Dbset.AsNoTracking().FirstOrDefault(t => t.Cnpj.Numero == cnpj);
        }

        public Paged<Cliente> GetPaged(int pageNumber, int pageSize, string search)
        {
            var skip = (pageNumber - 1) * pageSize;

            var paged = new Paged<Cliente>
            {
                List = Dbset.AsNoTracking()
                    .Where(t => t.Cnpj.Numero.Contains(search) || t.RazaoSocial.Contains(search) || t.NomeFantasia.Contains(search))
                    .OrderBy(t => t.NomeFantasia).Skip(skip).Take(pageSize),
                Total = Dbset.AsNoTracking()
                    .Count(t => t.Cnpj.Numero.Contains(search) || t.RazaoSocial.Contains(search) || t.NomeFantasia.Contains(search))
             };

            return paged;
        }

        public IEnumerable<Cliente> GetToNfeSaida(string search)
        {
                return Dbset.AsNoTracking().Where(c => c.Cnpj.Numero.Contains(search.ReplaceNull("")) ||
                    c.NomeFantasia.Contains(search.ReplaceNull(""))).Include(c => c.Enderecos).AsNoTracking()
                    .Include(c => c.Transportadora).AsNoTracking().OrderBy(c => c.NomeFantasia);
        }
        #endregion

        #region ClienteEndereço
        public ClienteEndereco GetEndereco(Guid clienteId)
        {
            return Db.ClienteEnderecos.AsNoTracking().FirstOrDefault(t => t.Id == clienteId);
        }

        public IEnumerable<ClienteEndereco> GetEnderecos(Guid clienteId)
        {
            return Db.ClienteEnderecos.AsNoTracking().Where(c => c.ClienteId == clienteId);
        }

        public ClienteEndereco AddEndereco(ClienteEndereco endereco)
        {
            return Db.ClienteEnderecos.Add(endereco).Entity;
        }

        public ClienteEndereco UpdateEndereco(ClienteEndereco endereco)
        {
            return Db.ClienteEnderecos.Update(endereco).Entity;
        }

        public ClienteEndereco DeleteEndereco(ClienteEndereco endereco)
        {
            endereco.Delete();
            return Db.ClienteEnderecos.Update(endereco).Entity;
        }
        #endregion

        #region ClienteTelefone
        public ClienteTelefone GetTelefone(Guid telefoneId)
        {
            return Db.ClienteTelefones.FirstOrDefault(t => t.Id == telefoneId);
        }

        public IEnumerable<ClienteTelefone> GetTelefones(Guid clienteId)
        {
            return Db.ClienteTelefones.Where(t => t.ClienteId == clienteId);
        }

        public ClienteTelefone AddTelefone(ClienteTelefone telefone)
        {
            return Db.ClienteTelefones.Add(telefone).Entity;
        }

        public ClienteTelefone UpdateTelefone(ClienteTelefone telefone)
        {
            return Db.ClienteTelefones.Update(telefone).Entity;
        }

        public ClienteTelefone DeleteTelefone(ClienteTelefone telefone)
        {
            telefone.Delete();
            return Db.ClienteTelefones.Update(telefone).Entity;
        }
        #endregion
    }
}

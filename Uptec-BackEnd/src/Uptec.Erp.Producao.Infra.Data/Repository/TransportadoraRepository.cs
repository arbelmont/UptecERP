using System;
using System.Collections.Generic;
using System.Linq;
using Definitiva.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Uptec.Erp.Producao.Domain.Transportadoras.Interfaces;
using Uptec.Erp.Producao.Domain.Transportadoras.Models;
using Uptec.Erp.Producao.Infra.Data.Context;

namespace Uptec.Erp.Producao.Infra.Data.Repository
{
    public class TransportadoraRepository : Repository<Transportadora>, ITransportadoraRepository
    {
        public TransportadoraRepository(ProducaoContext context) : base(context) {}

        #region Transportadora
        public Transportadora GetByIdWithAggregate(Guid id)
        {
            return null;
            //return Dbset.AsNoTracking().Include(t => t.Endereco).Include(t => t.Telefone)
            //    .FirstOrDefault(t => t.Id == id);
        }

        public Transportadora GetByCnpj(string cnpj)
        {
            return Dbset.AsNoTracking().FirstOrDefault(t => t.Cnpj.Numero == cnpj);
        }

        public Paged<Transportadora> GetPaged(int pageNumber, int pageSize, string search)
        {
            var skip = (pageNumber - 1) * pageSize;

            var paged = new Paged<Transportadora>
            {
                List = Dbset.AsNoTracking()
                    .Where(t => t.Cnpj.Numero.Contains(search) || t.RazaoSocial.Contains(search) || t.NomeFantasia.Contains(search))
                    .OrderBy(t => t.NomeFantasia).Skip(skip).Take(pageSize),
                Total = Dbset.AsNoTracking()
                    .Count(t => t.Cnpj.Numero.Contains(search) || t.RazaoSocial.Contains(search) || t.NomeFantasia.Contains(search))
                    
            };

            return paged;
        }
        #endregion

        #region TransportadoraEndereco
        public TransportadoraEndereco GetEndereco(Guid enderecoId)
        {
            return Db.TransportadoraEnderecos.FirstOrDefault(t => t.Id == enderecoId);
        }

        public IEnumerable<TransportadoraEndereco> GetEnderecos(Guid transportadoraId)
        {
            return Db.TransportadoraEnderecos.Where(t => t.TransportadoraId == transportadoraId);
        }

        public TransportadoraEndereco AddEndereco(TransportadoraEndereco endereco)
        {
            return Db.TransportadoraEnderecos.Add(endereco).Entity;
        }

        public TransportadoraEndereco UpdateEndereco(TransportadoraEndereco endereco)
        {
            return Db.TransportadoraEnderecos.Update(endereco).Entity;
        }

        public TransportadoraEndereco DeleteEndereco(TransportadoraEndereco endereco)
        {
            endereco.Delete();
            return Db.TransportadoraEnderecos.Update(endereco).Entity;
        }

        public TransportadoraTelefone GetTelefone(Guid telefoneId)
        {
            return Db.TransportadoraTelefones.FirstOrDefault(t => t.Id == telefoneId);
        }

        public IEnumerable<TransportadoraTelefone> GetTelefones(Guid transportadoraId)
        {
            return Db.TransportadoraTelefones.Where(t => t.TransportadoraId == transportadoraId);
        }
        #endregion

        #region Transportadora
        public TransportadoraTelefone AddTelefone(TransportadoraTelefone telefone)
        {
            return Db.TransportadoraTelefones.Add(telefone).Entity;
        }

        public TransportadoraTelefone UpdateTelefone(TransportadoraTelefone telefone)
        {
            return Db.TransportadoraTelefones.Update(telefone).Entity;
        }

        public TransportadoraTelefone DeleteTelefone(TransportadoraTelefone telefone)
        {
            telefone.Delete();
            return Db.TransportadoraTelefones.Update(telefone).Entity;
        }
        #endregion
    }
}

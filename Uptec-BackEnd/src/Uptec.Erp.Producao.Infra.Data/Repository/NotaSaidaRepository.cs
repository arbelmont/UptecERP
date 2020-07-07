using Definitiva.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data.SqlClient;
using System.Linq;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Infra.Data.Context;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Endereco;

namespace Uptec.Erp.Producao.Infra.Data.Repository
{
    public class NotaSaidaRepository : Repository<NotaSaida>, INotaSaidaRepository
    {
        public NotaSaidaRepository(ProducaoContext context)
            : base(context)
        {

        }

        public NotaSaida GetByNumeroNota(string numeroNota)
        {
            return Dbset.AsNoTracking().FirstOrDefault(n => n.NumeroNota == numeroNota);
        }

        public Paged<NotaSaida> GetPaged(int pageNumber, int pageSize, int tipoDestinatario, 
            int status, DateTime startDate, DateTime endDate, string search)
        {
            var skip = (pageNumber - 1) * pageSize;

            var paged = new Paged<NotaSaida>();

            paged.List = Dbset.AsNoTracking().Include(n => n.Cliente).AsNoTracking().Include(n => n.Fornecedor).AsNoTracking()
                .Where(n => n.Data >= startDate && n.Data.Date <= endDate &&
                      (status > 0 ? n.Status == (StatusNfSaida)status : true) &&
                      (tipoDestinatario > 0 ? n.TipoDestinatario == (TipoDestinatario)tipoDestinatario : true))
                .Where(n => n.Cliente.NomeFantasia.Contains(search) || n.Cliente.Cnpj.Numero.Contains(search) ||
                        n.Fornecedor.NomeFantasia.Contains(search) || n.Fornecedor.Cnpj.Numero.Contains(search) ||
                        n.NumeroNota.Contains(search))
                .OrderByDescending(n => n.Data).Skip(skip).Take(pageSize);
            paged.Total = Dbset.AsNoTracking()
                .Where(n => n.Cliente.NomeFantasia.Contains(search) || n.Cliente.Cnpj.Numero.Contains(search) ||
                        n.Fornecedor.NomeFantasia.Contains(search) || n.Fornecedor.Cnpj.Numero.Contains(search) ||
                        n.NumeroNota.Contains(search))
                .Count(n => n.Data >= startDate && n.Data.Date <= endDate &&
                      (status > 0 ? n.Status == (StatusNfSaida)status : true) &&
                      (tipoDestinatario > 0 ? n.TipoDestinatario == (TipoDestinatario)tipoDestinatario : true));

            return paged;    
        }

        public int GetNotaSaidaSequence()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            Db.Database.ExecuteSqlCommand(
                       "SELECT @result = (NEXT VALUE FOR NotaSaidaSequence)", result);

            return (int)result.Value;
        }

        public NotaSaida GetByIdWithAggregate(Guid id)
        {
            var nota = Dbset.AsNoTracking()
                .Include(n => n.Cliente).AsNoTracking()
                .Include(n => n.Fornecedor).AsNoTracking()
                .Include(n => n.Itens).AsNoTracking()
                .Include(n => n.Transportadora).AsNoTracking()
                .FirstOrDefault(n => n.Id == id);

            nota.Itens.OrderBy(i => i.TipoItem);

            return nota;
        }

        public Endereco GetEndereco(Guid enderecoId, TipoDestinatario tipoDestinatario)
        {
            Endereco endereco = null;

            if (tipoDestinatario == TipoDestinatario.Cliente)
                endereco = Db.ClienteEnderecos.AsNoTracking().FirstOrDefault(e => e.Id == enderecoId);
            else
                endereco = Db.FornecedorEnderecos.AsNoTracking().FirstOrDefault(e => e.Id == enderecoId);

            return endereco;
        }

        public Endereco GetEnderecoTransportadora(Guid transportadoraId)
        {
            return Db.TransportadoraEnderecos.AsNoTracking().FirstOrDefault(e => e.TransportadoraId == transportadoraId);
        }

        public NotaSaida GetByNumeroNotaWithAggregate(string numeroNota)
        {
            return Dbset.AsNoTracking()
                .Include(n => n.Cliente).AsNoTracking()
                .Include(n => n.Cliente.Enderecos).AsNoTracking()
                .Include(n => n.Fornecedor).AsNoTracking()
                .Include(n => n.Fornecedor.Enderecos).AsNoTracking()
                .Include(n => n.Itens).AsNoTracking()
                .FirstOrDefault(n => n.NumeroNota == numeroNota);
        }

        public void UpdateStatus(NotaSaida nota)
        {
            var sql = "UPDATE NotasSaida SET Status = @status, ErroApi = @erroApi WHERE Id = @id";
            var status = new SqlParameter("@status", nota.Status);
            var erroApi = new SqlParameter("@erroApi", nota.ErroApi);
            var id = new SqlParameter("@id", nota.Id.ToString());

            Db.Database.ExecuteSqlCommand(sql, parameters: new[] { status, erroApi, id });
        }
    }
}

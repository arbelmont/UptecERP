using Definitiva.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Uptec.Erp.Producao.Domain.Ordens.Interfaces;
using Uptec.Erp.Producao.Domain.Ordens.Models;
using Uptec.Erp.Producao.Infra.Data.Context;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Infra.Data.Repository
{
    public class OrdemRepository : Repository<Ordem>, IOrdemRepository
    {
        public OrdemRepository(ProducaoContext context)
            :base(context)
        {

        }

        public Ordem GetFullById(Guid id)
        {
            return Dbset.AsNoTracking().Where(o => o.Id == id)
                    .Include(o => o.OrdemLotes).AsNoTracking()
                    .FirstOrDefault();
        }

        public Ordem GetSuperFullById(Guid id)
        {
            return Dbset.AsNoTracking().Where(o => o.Id == id)
                    .Include(o => o.OrdemLotes)
                    .ThenInclude(ol => ol.Lote)
                    .ThenInclude(l => l.Peca).AsNoTracking()
                    .FirstOrDefault();
        }

        public Paged<Ordem> GetPaged(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;

            var paged = new Paged<Ordem>
            {
                List = Dbset.AsNoTracking()
                .Include(o => o.OrdemLotes).AsNoTracking()
                .OrderByDescending(o => o.DataEmissao).Skip(skip).Take(pageSize),
                    
                Total = Dbset.AsNoTracking().Count()
            };

            return paged;
        }

        public IEnumerable<OrdemLote> GetOrdemLotesToNfeSaida(Guid clienteId)
        {
            return Db.OrdemLotes.AsNoTracking().Include(ol => ol.Ordem).AsNoTracking().Include(ol => ol.Lote).AsNoTracking()
                .Where(ol => ol.MotivoExpedicao != OrdemMotivoExpedicao.Produzindo && ol.NotaFiscalSaida == null)
                .Where(l => l.Lote.ClienteId == clienteId)
                .Where(o => o.Ordem.Status == StatusOrdem.Expedicao);
        }

        public Paged<Ordem> GetPaged(int pageNumber, int pageSize, int status, string campo, string search)
        {
            var skip = (pageNumber - 1) * pageSize;

            //var paged = new Paged<Ordem>();
            Paged<Ordem> paged = null;

            if (search == string.Empty)
            {
                paged = new Paged<Ordem>
                {
                    List = Dbset.AsNoTracking().Where(o => status <= 0 || o.Status == (StatusOrdem)status)
                    .Include(o => o.OrdemLotes).AsNoTracking()
                    .OrderByDescending(o => o.DataEmissao).Skip(skip).Take(pageSize),

                    Total = Dbset.AsNoTracking().Count(o => status <= 0 || o.Status == (StatusOrdem)status)
                };
            }
            else
            {
                switch (campo)
                {
                    case "peca":
                        {
                            paged = new Paged<Ordem>
                            {
                                List = Dbset.AsNoTracking()
                                        .Where(o => status <= 0 || o.Status == (StatusOrdem)status)
                                        .Where(o => o.CodigoPeca.Contains(search) || o.DescricaoPeca.Contains(search))
                                        .Include(o => o.OrdemLotes).AsNoTracking()
                                        .OrderByDescending(o => o.DataEmissao).Skip(skip).Take(pageSize),

                                Total = Dbset.AsNoTracking()
                                .Where(o => status <= 0 || o.Status == (StatusOrdem)status)
                                .Where(o => o.CodigoPeca.Contains(search) || o.DescricaoPeca.Contains(search))
                                .Count()
                                        
                            };
                            break;
                        }
                    case "lote":
                        {
                            int.TryParse(search, out int intLote);

                            paged = new Paged<Ordem>
                            {
                                List = Dbset.AsNoTracking()
                                        .Where(o => status <= 0 || o.Status == (StatusOrdem)status)
                                        .Include(o => o.OrdemLotes).AsNoTracking()
                                        .Where(o => o.OrdemLotes.Any(l => l.LoteNumero == intLote))
                                        .OrderByDescending(o => o.DataEmissao).Skip(skip).Take(pageSize),

                                Total = Dbset.AsNoTracking()
                                .Where(o => status <= 0 || o.Status == (StatusOrdem)status)
                                .Include(o => o.OrdemLotes).AsNoTracking()
                                .Where(o => o.OrdemLotes.Any(l => l.LoteNumero == intLote))
                                .Count()

                            };
                            break;
                        }
                    case "ordem":
                        {
                            int.TryParse(search, out int intOrdem);

                            paged = new Paged<Ordem>
                            {
                                List = Dbset.AsNoTracking()
                                        .Where(o => status <= 0 || o.Status == (StatusOrdem)status)
                                        .Where(o => o.OrdemNumero == intOrdem)
                                        .Include(o => o.OrdemLotes).AsNoTracking()
                                        .OrderByDescending(o => o.DataEmissao).Skip(skip).Take(pageSize),

                                Total = Dbset.AsNoTracking()
                                .Where(o => status <= 0 || o.Status == (StatusOrdem)status)
                                .Where(o => o.OrdemNumero == intOrdem)
                                .Count()

                            };
                            break;
                        }
                    default:
                        break;
                }
            }

            return paged ?? new Paged<Ordem>();
        }

        public OrdemLote UpdateOrdemLote(OrdemLote ordemLote)
        {
            return Db.OrdemLotes.Update(ordemLote).Entity;
        }

        public int GetOrdemSequence()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            Db.Database.ExecuteSqlCommand("SELECT @result = (NEXT VALUE FOR OrdemSequence)", result);

            return (int)result.Value;
        }

        public OrdemLote GetOrdemLoteById(Guid ordemLoteId)
        {
            return Db.OrdemLotes.FirstOrDefault(ol => ol.Id == ordemLoteId);
        }

        public OrdemLote GetOrdemLoteFullById(Guid ordemLoteId)
        {
            return Db.OrdemLotes.AsNoTracking().Include(ol => ol.Ordem).AsNoTracking().FirstOrDefault(ol => ol.Id == ordemLoteId);
        }

        public void UpdateStatus(Ordem ordem)
        {
            var sql = "UPDATE Ordens SET Status = @status WHERE Id = @id";
            var status = new SqlParameter("@status", ordem.Status);
            var id = new SqlParameter("@id", ordem.Id.ToString());

            Db.Database.ExecuteSqlCommand(sql, parameters: new[] { status, id });
        }

        public IEnumerable<LinhaProducao> GetLinhaProducao()
        {
            var sql =  "select Id, Cliente, Materia, Produto, " +
                    "sum(isnull(Producao,0)+isnull(Produzido,0)+isnull(Saldo,0))  Entrada, " +
                    "sum(Saldo) Saldo, sum(producao) Producao, sum(produzido) Expedicao " +
                    "from ( select c.NomeFantasia Cliente, p.id Id, p.Codigo Materia, p.CodigoSaida Produto, sum(l.quantidade) Entrada, sum(Saldo) Saldo, " +
                    "case when o.status = 2 then sum(ol.qtdeProduzida) end as Produzido, case when o.status= 1 then sum(o.qtdeTotal) end as producao " +
                    "from lotes l " + 
                    "inner join Pecas p with(nolock) on l.PecaId = p.Id and p.deleted = 0 " +
                    "inner join Clientes c with(nolock) on p.ClienteId = c.Id and p.deleted = 0 " +
                    "left join ordensLotes ol with(nolock) on l.Id = ol.LoteId and ol.deleted = 0 " +
                    "left join ordens o with(nolock) on o.Id = ol.OrdemId and o.deleted = 0 " +
                    "where (o.status <> 3 or l.status <> 2) and p.tipo = 1 and l.deleted = 0 " + 
                    "group by c.NomeFantasia, p.Id, p.Codigo,  p.CodigoSaida, o.status, ol.ordemId)A " +
                    "group by Id, Cliente, Produto, Materia " +
                    "order by Cliente";

            return Db.LinhaProducao.FromSql(sql);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Definitiva.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Producao.Infra.Data.Context;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Infra.Data.Repository
{
    public class LoteRepository : Repository<Lote>, ILoteRepository
    {
        public LoteRepository(ProducaoContext context) : base(context) { }
        public LoteMovimento AddMovimento(LoteMovimento movimento)
        {
            return Db.LoteMovimentos.Add(movimento).Entity;
        }

        public Lote GetByIdWithAggregate(Guid id)
        {
            return Dbset.AsNoTracking().Include(p => p.Peca).AsNoTracking().Include(c => c.Peca.Componentes).FirstOrDefault(l => l.Id == id);
        }

        public Lote GetByNumero(int numeroLote)
        {
            return Dbset.AsNoTracking().FirstOrDefault(l => l.LoteNumero == numeroLote);
        }

        public ICollection<Lote> GetByNumeroNota(string numeroNota)
        {
            return Dbset.AsNoTracking().Where(l => l.NotaFiscal == numeroNota).ToList();
        }

        public ICollection<Lote> GetByNumeroNotaCobertura(string numeroNota)
        {
            return Dbset.AsNoTracking().Where(l => l.NotaFiscalCobertura == numeroNota).ToList();
        }

        public ICollection<Lote> GetByNumeroNotaOuCobertura(string numeroNota)
        {
            return Dbset.AsNoTracking().Where(l => l.NotaFiscal == numeroNota || l.NotaFiscalCobertura == numeroNota).ToList();
        }

        public ICollection<Lote> GetByNumeroNotaOuCoberturaWithAggregate(string numeroNota)
        {
            return Dbset.AsNoTracking().Include(p => p.Peca).AsNoTracking()
                .Where(l => l.NotaFiscal == numeroNota || l.NotaFiscalCobertura == numeroNota).ToList();
        }

        public LoteMovimento GetMovimentoById(Guid movimentoId)
        {
            return Db.LoteMovimentos.FirstOrDefault(m => m.Id == movimentoId);
        }

        public Paged<Lote> GetPaged(int pageNumber, int pageSize, bool showLoteFechado, string search)
        {
            var skip = (pageNumber - 1) * pageSize;

            var hasLote = int.TryParse(search, out int lote);

            var paged = new Paged<Lote>();

            if(hasLote)
            {
                paged.List = Dbset.AsNoTracking().Include(p => p.Peca)
                    .Where(p => p.LoteNumero == lote || p.NotaFiscal.Contains(search) || p.Peca.Codigo.Contains(search) && (!showLoteFechado ? p.Status == Shared.Domain.Enums.LoteStatus.Aberto :true))
                    .OrderByDescending(p => p.Data).Skip(skip).Take(pageSize);
                paged.Total = Dbset.AsNoTracking()
                    .Count(p => p.LoteNumero == lote || p.NotaFiscal.Contains(search) || p.Peca.Codigo.Contains(search) && (!showLoteFechado ? p.Status == Shared.Domain.Enums.LoteStatus.Aberto : true));
            }
            else
            {
                paged.List = Dbset.AsNoTracking().Include(p => p.Peca)
                    .Where(p => p.NotaFiscal.Contains(search) || p.Peca.Descricao.Contains(search) || p.Peca.Codigo.Contains(search) && (!showLoteFechado ? p.Status == Shared.Domain.Enums.LoteStatus.Aberto : true))
                    .OrderByDescending(p => p.Data).Skip(skip).Take(pageSize);
                paged.Total = Dbset.AsNoTracking()
                    .Count(p => p.NotaFiscal.Contains(search) || p.Peca.Descricao.Contains(search) || p.Peca.Codigo.Contains(search) && (!showLoteFechado ? p.Status == Shared.Domain.Enums.LoteStatus.Aberto : true));
            }

            return paged;
        }

        public Paged<Lote> GetPaged(int pageNumber, int pageSize, Guid pecaId, bool showLoteFechado)
        {
            var skip = (pageNumber - 1) * pageSize;

            var paged = new Paged<Lote>
            {
                List = Dbset.AsNoTracking()
                    .Where(p => p.PecaId == pecaId && (!showLoteFechado ? p.Status == LoteStatus.Aberto : true))
                    .OrderBy(p => p.LoteNumero).Skip(skip).Take(pageSize),
                Total = Dbset.AsNoTracking()
                    .Count(p => p.PecaId == pecaId && (!showLoteFechado ? p.Status == LoteStatus.Aberto : true))
            };

            return paged;
        }

        public Paged<Lote> GetFullPagedByPeca(int pageNumber, int pageSize, Guid pecaId, bool showLoteFechado)
        {
            var skip = (pageNumber - 1) * pageSize;

            var paged = new Paged<Lote>
            {
                List = Dbset.AsNoTracking().Include(p => p.Peca).AsNoTracking()
                    .Where(p => p.PecaId == pecaId && (!showLoteFechado ? p.Status == LoteStatus.Aberto : true))
                    .OrderByDescending(p => p.Data).Skip(skip).Take(pageSize),
                Total = Dbset.AsNoTracking()
                    .Count(p => p.PecaId == pecaId && (!showLoteFechado ? p.Status == LoteStatus.Aberto : true))
            };

            return paged;
        }

        public Paged<LoteMovimento> GetPagedMovimento(Guid loteId, DateTime startDate, DateTime endDate, int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;

            var paged = new Paged<LoteMovimento>
            {
                List = Db.LoteMovimentos.AsNoTracking()
                    .Where(lm => lm.Data >= startDate && lm.Data.Date <= endDate && lm.LoteId == loteId)
                    .OrderByDescending(lm => lm.Data).Skip(skip).Take(pageSize),
                Total = Db.LoteMovimentos.AsNoTracking()
                    .Count(lm => lm.Data >= startDate && lm.Data.Date <= endDate && lm.LoteId == loteId)
            };

            return paged;
        }

        public Paged<LoteMovimento> GetPagedMovimento(Guid loteId, int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;

            var paged = new Paged<LoteMovimento>
            {
                List = Db.LoteMovimentos.AsNoTracking()
                    .Where(lm => lm.LoteId == loteId)
                    .OrderByDescending(lm => lm.Data).Skip(skip).Take(pageSize),
                Total = Db.LoteMovimentos.AsNoTracking()
                    .Count(lm => lm.LoteId == loteId)
            };

            return paged;
        }

        public IEnumerable<Lote> GetLotesEmbalagemToNfeSaida(Guid destinatarioId, TipoDestinatario tipoDestinatario)
        {
            if(tipoDestinatario == TipoDestinatario.Cliente)
            {
                return Dbset.AsNoTracking().Include(l => l.Peca).Where(l => l.Saldo > 0 && l.ClienteId == destinatarioId && l.TipoPeca == TipoPeca.Embalagem);
            }
            return Dbset.AsNoTracking().Include(l => l.Peca).Where(l => l.Saldo > 0 && l.FornecedorId == destinatarioId && l.TipoPeca == TipoPeca.Embalagem);
        }

        public IEnumerable<Lote> GetLotesPecaToNfeSaida(Guid destinatarioId, TipoDestinatario tipoDestinatario)
        {
            if(tipoDestinatario == TipoDestinatario.Cliente)
            {
                return Dbset.AsNoTracking().Include(l => l.Peca).Where(l => l.Saldo > 0 && l.ClienteId == destinatarioId && l.TipoPeca == TipoPeca.Peca);
            }
            return Dbset.AsNoTracking().Include(l => l.Peca).Where(l => l.Saldo > 0 && l.FornecedorId == destinatarioId && l.TipoPeca == TipoPeca.Peca);
        }

        public void UpdateSequenciaLote(Lote lote)
        {
            var sql = "UPDATE Lotes SET Sequencia = @sequencia WHERE Id = @id";
            var sequencia = new SqlParameter("@sequencia", lote.Sequencia);
            var id = new SqlParameter("@id", lote.Id.ToString());

            Db.Database.ExecuteSqlCommand(sql, parameters: new[] { sequencia, id });
        }

        public void UpdateLoteSaldo(Lote lote)
        {
            var sql = "UPDATE Lotes SET Saldo = @saldo, Status = @status WHERE Id = @id";
            var saldo = new SqlParameter("@saldo", lote.Saldo);
            var status = new SqlParameter("@status", lote.Status);
            var id = new SqlParameter("@id", lote.Id.ToString());

            Db.Database.ExecuteSqlCommand(sql, parameters: new[] { saldo, status, id });
        }

        //Metodo utilizado somente para o fechamento da consiliação (no serviço de notas), 
        //pois ja pega do banco o proximo numero de lote
        public int GetLoteSequence()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            Db.Database.ExecuteSqlCommand(
                       "SELECT @result = (NEXT VALUE FOR LoteSequence)", result);

            return (int)result.Value;
        }

        public int GetLoteSequenceLastUsed()
        {
            SqlParameter result = new SqlParameter("@result", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            Db.Database.ExecuteSqlCommand(
                "SELECT @result = (select CAST(current_value As int) FROM sys.sequences WHERE name = 'loteSequence');", result);

            return (int)result.Value;
        }

        public IEnumerable<LoteSaldo> GetLotesSaldo()
        {
            var sql = "select p.id, c.nomefantasia as cliente, p.codigo, p.descricao as produto, sum(l.quantidade) as entrada, sum(l.saldo) as saldo ";
                sql = sql + "from lotes l ";
                sql = sql + "inner join pecas p with(nolock) on p.id = l.pecaid and p.deleted = 0 ";
                sql = sql + "inner join clientes c with(nolock) on p.clienteId = c.id and c.deleted = 0";
                sql = sql + "where p.tipo = 2 and l.saldo > 0 and l.deleted = 0 ";
                sql = sql + "group by p.id, c.nomefantasia, p.codigo, p.descricao ";
                sql = sql + "order by c.nomefantasia ";

            return Db.LoteSaldo.FromSql(sql);
        }
    }
}

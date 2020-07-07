using Definitiva.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Producao.Infra.Data.Context;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Infra.Data.Repository
{
    public class NotaEntradaRepository : Repository<NotaEntrada>, INotaEntradaRepository
    {
        public NotaEntradaRepository(ProducaoContext context)
            : base(context)
        {

        }

        public NotaEntrada GetByIdWithAggregate(Guid id)
        {
            return Dbset.AsNoTracking().Include(n => n.Itens).AsNoTracking().FirstOrDefault(n => n.Id == id);
        }

        public NotaEntrada GetByNumeroNota(string numeroNota)
        {
            return Dbset.AsNoTracking().FirstOrDefault(p => p.NumeroNota == numeroNota);
        }

        public Lote GetLoteByNumeroNota(string numeroNota)
        {
            return Db.Lotes.AsNoTracking().FirstOrDefault(l => l.NotaFiscal == numeroNota);
        }

        public Paged<NotaEntrada> GetPaged(int pageNumber, int pageSize, int tipoEmissor, 
                                           int status, DateTime startDate, DateTime endDate, string search)
        {
            var skip = (pageNumber - 1) * pageSize;

            var paged = new Paged<NotaEntrada>();

            paged.List = Dbset.AsNoTracking()
                .Where(n => n.Data >= startDate && n.Data.Date <= endDate && 
                    (n.Cfop.Contains(search) || n.CnpjEmissor.Numero.Contains(search) || n.NomeEmissor.Contains(search) || n.NumeroNota.Contains(search)) &&
                    (tipoEmissor > 0? n.TipoEmissor == (TipoEmissor)tipoEmissor : true) &&
                    (status > 0 ? n.Status == (StatusNfEntrada)status : true))
                .OrderByDescending(n => n.Data).Skip(skip).Take(pageSize);
            paged.Total = Dbset.AsNoTracking()
                .Count(n => n.Data >= startDate && n.Data.Date <= endDate && 
                (n.Cfop.Contains(search) || n.CnpjEmissor.Numero.Contains(search) || n.NomeEmissor.Contains(search) || n.NumeroNota.Contains(search)) &&
                (tipoEmissor > 0 ? n.TipoEmissor == (TipoEmissor)tipoEmissor : true) &&
                (status > 0 ? n.Status == (StatusNfEntrada)status : true));

            return paged;
        }

        public int GetQtdeNotasAcobrir()
        {
            return Dbset.AsNoTracking().Count(n => n.Status == StatusNfEntrada.ACobrir);
        }

        public List<NotaEntrada> GetNotasClientesConciliar()
        {
            return Dbset.AsNoTracking().Where(n => n.TipoEmissor == TipoEmissor.Cliente && n.Status == StatusNfEntrada.Conciliar).ToList();
        }

        public NotaEntradaItens UpdateItem(NotaEntradaItens item)
        {
            return Db.NotaEntradaItens.Update(item).Entity;
        }
    }
}

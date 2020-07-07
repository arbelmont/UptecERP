using Definitiva.Shared.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Uptec.Erp.Producao.Infra.Data.Context;

namespace Uptec.Erp.Producao.Infra.Data.UoW
{
    public class UnitOfWok : IUnitOfWork
    {
        private readonly ProducaoContext _context;

        public UnitOfWok(ProducaoContext context)
        {
            _context = context;
        }

        public void StartTransaction()
        {
            if (_context.Database.CurrentTransaction == null)
                _context.Database.BeginTransaction();
        }
        public bool Commit()
        {
            if (_context.SaveChanges() > 0)
            {
                if (_context.Database.CurrentTransaction != null)
                    _context.Database.CommitTransaction();

                return true;
            }

            RollbackTransaction();

            return false;
        }

        public void RollbackTransaction()
        {
            if (_context.Database.CurrentTransaction != null)
                _context.Database.RollbackTransaction();
        }

        public void CommitTransaction()
        {
            if (_context.Database.CurrentTransaction != null)
                _context.Database.CommitTransaction();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}

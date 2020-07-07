using System;
using System.Collections.Generic;
using System.Linq;
using Definitiva.Shared.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Uptec.Erp.Producao.Infra.Data.Context;
using Uptec.Erp.Shared.Domain.Interfaces;

namespace Uptec.Erp.Producao.Infra.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : Entity<T>
    {
        protected ProducaoContext Db;
        protected DbSet<T> Dbset;

        public Repository(ProducaoContext context)
        {
            Db = context;
            Dbset = Db.Set<T>();
        }

        public T Add(T obj)
        {
            return Dbset.Add(obj).Entity;
        }

        public T Update(T obj)
        {
            return Dbset.Update(obj).Entity;
        }

        public virtual T Delete(T obj)
        {
            return Dbset.Update(obj).Entity;
        }

        public T GetById(Guid id)
        {
            return Dbset.AsNoTracking().FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<T> GetAll(bool showDeleted = false)
        {
            return Dbset.AsNoTracking().Where(t => t.Deleted == showDeleted);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        public void Dispose()
        {
            Db.Dispose();
        }
    }
}

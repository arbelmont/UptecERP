using System;
using System.Collections.Generic;
using Definitiva.Shared.Domain.Models;

namespace Uptec.Erp.Shared.Domain.Interfaces
{
    public interface IRepository<T> : IDisposable where T : Entity<T>
    {
        T Add(T obj);
        T Update(T obj);
        T Delete(T obj);
        T GetById(Guid id);
        IEnumerable<T> GetAll(bool showDeleted = false);
        int SaveChanges();
    }
}
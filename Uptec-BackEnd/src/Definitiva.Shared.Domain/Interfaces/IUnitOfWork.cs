using System;

namespace Definitiva.Shared.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        void StartTransaction();

        bool Commit();

        void RollbackTransaction();

        void CommitTransaction();
    }
}

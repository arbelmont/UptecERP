using System;
using Uptec.Erp.Producao.Domain.Pecas.Models;

namespace Uptec.Erp.Producao.Domain.Pecas.Interfaces
{
    public interface IPecaService : IDisposable
    {
        void Add(Peca peca);
        void Update(Peca peca);
        void Delete(Guid id);
    }
}
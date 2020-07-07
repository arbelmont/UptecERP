using System;
using Uptec.Erp.Producao.Domain.Arquivos.Models;

namespace Uptec.Erp.Producao.Domain.Arquivos.Interfaces
{
    public interface IArquivoService : IDisposable
    {
        void Add(Arquivo arquivo);
    }
}
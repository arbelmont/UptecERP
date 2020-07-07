using System;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Fiscal.Interfaces
{
    public interface INotaEntradaService : IDisposable
    {
        void Add(NotaEntrada notaEntrada);
        void Update(NotaEntrada notaEntrada);
        void Delete(Guid id);
        void DefinirTipoEmissor(Guid id, TipoEmissor tipo);
        void Conciliar(NotaEntrada notaEntrada);
        void Cobrir(Guid notaFornecedorId, Guid notaClienteId);
        //void UpdateNumeroLote(Guid notaEntradaId);
        NotaEntrada GetConsistida(Guid id);
    }
}
using System;
using Uptec.Erp.Producao.Domain.Transportadoras.Models;

namespace Uptec.Erp.Producao.Domain.Transportadoras.Interfaces
{
    public interface ITransportadoraService : IDisposable
    {
        void Add(Transportadora transportadora);
        void Update(Transportadora transportadora);
        void Delete(Guid id);

        void AddEndereco(TransportadoraEndereco endereco);
        void UpdateEndereco(TransportadoraEndereco endereco);
        void DeleteEndereco(Guid id);

        void AddTelefone(TransportadoraTelefone telefone);
        void UpdateTelefone(TransportadoraTelefone telefone);
        void DeleteTelefone(Guid id);
    }
}
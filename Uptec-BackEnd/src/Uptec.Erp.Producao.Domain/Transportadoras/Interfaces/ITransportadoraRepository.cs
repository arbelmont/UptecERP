using System;
using System.Collections.Generic;
using Definitiva.Shared.Domain.Models;
using Uptec.Erp.Producao.Domain.Transportadoras.Models;
using Uptec.Erp.Shared.Domain.Interfaces;

namespace Uptec.Erp.Producao.Domain.Transportadoras.Interfaces
{
    public interface ITransportadoraRepository : IRepository<Transportadora>
    {
        Transportadora GetByIdWithAggregate(Guid id);
        Transportadora GetByCnpj(string cnpj);
        Paged<Transportadora> GetPaged(int pageNumber, int pageSize, string search);

        TransportadoraEndereco GetEndereco(Guid enderecoId);
        IEnumerable<TransportadoraEndereco> GetEnderecos(Guid transportadoraId);
        TransportadoraEndereco AddEndereco(TransportadoraEndereco endereco);
        TransportadoraEndereco UpdateEndereco(TransportadoraEndereco endereco);
        TransportadoraEndereco DeleteEndereco(TransportadoraEndereco endereco);

        TransportadoraTelefone GetTelefone(Guid telefoneId);
        IEnumerable<TransportadoraTelefone> GetTelefones(Guid transportadoraId);
        TransportadoraTelefone AddTelefone(TransportadoraTelefone telefone);
        TransportadoraTelefone UpdateTelefone(TransportadoraTelefone telefone);
        TransportadoraTelefone DeleteTelefone(TransportadoraTelefone telefone);
    }
}

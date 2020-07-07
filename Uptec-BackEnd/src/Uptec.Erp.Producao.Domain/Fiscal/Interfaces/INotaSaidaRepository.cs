using Definitiva.Shared.Domain.Models;
using System;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Interfaces;
using Uptec.Erp.Shared.Domain.Models.Endereco;

namespace Uptec.Erp.Producao.Domain.Fiscal.Interfaces
{
    public interface INotaSaidaRepository : IRepository<NotaSaida>
    {
        NotaSaida GetByNumeroNota(string numeroNota);
        NotaSaida GetByNumeroNotaWithAggregate(string numeroNota);
        int GetNotaSaidaSequence();
        NotaSaida GetByIdWithAggregate(Guid id);
        //Lote GetLoteByNumeroNota(string numeroNota);
        Paged<NotaSaida> GetPaged(int pageNumber, int pageSize, int tipoDestinatario, int status, DateTime startDate, DateTime endDate, string search);
        Endereco GetEndereco(Guid enderecoId, TipoDestinatario tipoDestinatario);
        Endereco GetEnderecoTransportadora(Guid transportadoraId);
        void UpdateStatus(NotaSaida nota);
    }
}
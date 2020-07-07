using Definitiva.Shared.Domain.DomainValidator;
using System;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;
using Uptec.Erp.Producao.Domain.Ordens.Models;

namespace Uptec.Erp.Producao.Domain.Ordens.Specifications
{
    class OrdemLoteQtdeSuficienteSpec : IDomainSpecification<Ordem>
    {
        private readonly ILoteRepository _loteRepository;
        private readonly Guid _loteId;
        private readonly decimal _qtde;

        public OrdemLoteQtdeSuficienteSpec(ILoteRepository loteRepository, Guid loteId, decimal qtde)
        {
            _loteRepository = loteRepository;
            _loteId = loteId;
            _qtde = qtde;
        }

        public bool IsSatisfiedBy(Ordem ordem)
        {
            var lote = _loteRepository.GetById(_loteId);

            if (lote == null)
                return true;

            return lote.Saldo >= _qtde;
        }
    }
}

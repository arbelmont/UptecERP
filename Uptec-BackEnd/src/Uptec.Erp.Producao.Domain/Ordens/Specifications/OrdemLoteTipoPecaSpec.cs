using Definitiva.Shared.Domain.DomainValidator;
using System;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;
using Uptec.Erp.Producao.Domain.Ordens.Models;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Ordens.Specifications
{
    class OrdemLoteTipoPecaSpec : IDomainSpecification<Ordem>
    {
        private readonly ILoteRepository _loteRepository;
        private readonly Guid _loteId;

        public OrdemLoteTipoPecaSpec(ILoteRepository loteRepository, Guid loteId)
        {
            _loteRepository = loteRepository;
            _loteId = loteId;
        }

        public bool IsSatisfiedBy(Ordem ordem)
        {
            var lote = _loteRepository.GetById(_loteId);

            if (lote == null) return true;

            return lote.TipoPeca ==  TipoPeca.Peca;
        }
    }
}

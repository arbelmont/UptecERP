using Definitiva.Shared.Domain.DomainValidator;
using System;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;
using Uptec.Erp.Producao.Domain.Ordens.Models;

namespace Uptec.Erp.Producao.Domain.Ordens.Specifications
{
    class OrdemLoteExistsSpec : IDomainSpecification<Ordem>
    {
        private readonly ILoteRepository _loteRepository;
        private readonly Guid _loteId;

        public OrdemLoteExistsSpec(ILoteRepository loteRepository, Guid loteId)
        {
            _loteRepository = loteRepository;
            _loteId = loteId;
        }

        public bool IsSatisfiedBy(Ordem ordem)
        {
            return _loteRepository.GetById(_loteId) != null;
        }
    }
}

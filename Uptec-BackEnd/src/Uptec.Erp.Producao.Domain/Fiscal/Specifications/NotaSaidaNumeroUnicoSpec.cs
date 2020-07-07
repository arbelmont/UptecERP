using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaSaidaNumeroUnicoSpec : IDomainSpecification<NotaSaida>
    {
        private readonly INotaSaidaRepository _notaSaidaRepository;
        private readonly DomainOperation _domainOperation;

        public NotaSaidaNumeroUnicoSpec(INotaSaidaRepository notaSaidaRepository, DomainOperation domainOperation)
        {
            _notaSaidaRepository = notaSaidaRepository;
            _domainOperation = domainOperation;
        }

        public bool IsSatisfiedBy(NotaSaida entity)
        {
            var nota = _notaSaidaRepository.GetByNumeroNota(entity.NumeroNota);

            if (_domainOperation == DomainOperation.Add)
                return nota == null;

            return nota == null || nota.Id == entity.Id;
        }
    }
}

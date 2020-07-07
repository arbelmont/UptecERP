using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaEntradaNumeroUnicoSpec : IDomainSpecification<NotaEntrada>
    {
        private readonly INotaEntradaRepository _notaEntradaRepository;
        private readonly DomainOperation _domainOperation;

        public NotaEntradaNumeroUnicoSpec(INotaEntradaRepository notaEntradaRepository, DomainOperation domainOperation)
        {
            _notaEntradaRepository = notaEntradaRepository;
            _domainOperation = domainOperation;
        }

        public bool IsSatisfiedBy(NotaEntrada entity)
        {
            var nota = _notaEntradaRepository.GetByNumeroNota(entity.NumeroNota);

            if (_domainOperation == DomainOperation.Add)
                return nota == null;

            return nota == null || nota.Id == entity.Id;
        }
    }
}

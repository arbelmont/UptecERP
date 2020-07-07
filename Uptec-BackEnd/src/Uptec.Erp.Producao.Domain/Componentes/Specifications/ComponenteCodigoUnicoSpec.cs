using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Componentes.Interfaces;
using Uptec.Erp.Producao.Domain.Componentes.Models;

namespace Uptec.Erp.Producao.Domain.Componentes.Specifications
{
    public class ComponenteCodigoUnicoSpec : IDomainSpecification<Componente>
    {
        private readonly IComponenteRepository _componenteRepository;
        private readonly DomainOperation _domainOperation;

        public ComponenteCodigoUnicoSpec(IComponenteRepository componenteRepository, DomainOperation domainOperation)
        {
            _componenteRepository = componenteRepository;
            _domainOperation = domainOperation;
        }

        public bool IsSatisfiedBy(Componente entity)
        {
            var componente = _componenteRepository.GetByCodigo(entity.Codigo);

            if (_domainOperation == DomainOperation.Add)
                return componente == null;

            return componente == null || componente.Id == entity.Id;
        }
    }
}

using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Componentes.Models;

namespace Uptec.Erp.Producao.Domain.Componentes.Specifications
{
    public class ComponenteQuantidadeSuficienteSpec : IDomainSpecification<ComponenteMovimento>
    {
        private readonly Componente _componente;

        public ComponenteQuantidadeSuficienteSpec(Componente componente)
        {
            _componente = componente;
        }

        public bool IsSatisfiedBy(ComponenteMovimento entity)
        {
            return entity.Quantidade <= _componente.Quantidade;
        }
    }
}

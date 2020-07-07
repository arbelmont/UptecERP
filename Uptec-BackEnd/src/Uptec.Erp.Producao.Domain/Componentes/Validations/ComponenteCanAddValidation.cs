using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Componentes.Interfaces;
using Uptec.Erp.Producao.Domain.Componentes.Models;
using Uptec.Erp.Producao.Domain.Componentes.Specifications;

namespace Uptec.Erp.Producao.Domain.Componentes.Validations
{
    public class ComponenteCanAddValidation : DomainValidator<Componente>
    {
        public ComponenteCanAddValidation(IComponenteRepository componenteRepository)
        {
            Add(new ComponenteCodigoUnicoSpec(componenteRepository, DomainOperation.Add), "Componente já cadastrado.");
        }
    }
}
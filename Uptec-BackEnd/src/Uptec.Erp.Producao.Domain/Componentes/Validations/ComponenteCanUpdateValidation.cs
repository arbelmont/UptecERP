using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Componentes.Interfaces;
using Uptec.Erp.Producao.Domain.Componentes.Models;
using Uptec.Erp.Producao.Domain.Componentes.Specifications;

namespace Uptec.Erp.Producao.Domain.Componentes.Validations
{
    public class ComponenteCanUpdateValidation : DomainValidator<Componente>
    {
        public ComponenteCanUpdateValidation(IComponenteRepository componenteRepository)
        {
            Add(new ComponenteCodigoUnicoSpec(componenteRepository, DomainOperation.Update), "Componente já cadastrado.");
        }
    }
}
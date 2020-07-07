using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Componentes.Models;
using Uptec.Erp.Producao.Domain.Componentes.Specifications;

namespace Uptec.Erp.Producao.Domain.Componentes.Validations
{
    public class ComponenteMovimentoCanAddSaidaValidation : DomainValidator<ComponenteMovimento>
    {
        public ComponenteMovimentoCanAddSaidaValidation(Componente componente)
        {
            Add(new ComponenteQuantidadeSuficienteSpec(componente), "Quantidade insuficiente no estoque de matéria prima.");
        }
    }
}
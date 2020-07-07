using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Componentes.Interfaces;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaEntradaComponenteUnidadeCompativelSpec : IDomainSpecification<NotaEntrada>
    {
        private readonly IComponenteRepository _componenteRepository;
        private readonly NotaEntradaItens _item;

        public NotaEntradaComponenteUnidadeCompativelSpec(IComponenteRepository componenteRepository, NotaEntradaItens item)
        {
            _componenteRepository = componenteRepository;
            _item = item;
        }

        public bool IsSatisfiedBy(NotaEntrada entity)
        {
            if (_item == null) return true;

            var componente = _componenteRepository.GetByCodigo(_item.Codigo);

            if (componente == null) return true;

            return componente.Unidade == _item.Unidade;
        }
    }
}

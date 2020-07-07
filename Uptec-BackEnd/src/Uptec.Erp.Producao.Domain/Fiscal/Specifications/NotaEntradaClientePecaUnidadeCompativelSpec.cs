using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Pecas.Interfaces;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaEntradaClientePecaUnidadeCompativelSpec : IDomainSpecification<NotaEntrada>
    {
        private readonly IPecaRepository _pecaRepository;
        private readonly NotaEntradaItens _item;

        public NotaEntradaClientePecaUnidadeCompativelSpec(IPecaRepository pecaRepository, NotaEntradaItens item)
        {
            _pecaRepository = pecaRepository;
            _item = item;
        }

        public bool IsSatisfiedBy(NotaEntrada entity)
        {
            if (_item == null) return true;

            var peca = _pecaRepository.GetByCodigo(_item.Codigo);

            if (peca == null) return true;

            return peca.Unidade == _item.Unidade;
        }
    }
}

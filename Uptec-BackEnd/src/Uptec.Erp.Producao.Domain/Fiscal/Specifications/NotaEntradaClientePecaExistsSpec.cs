using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Pecas.Interfaces;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaEntradaClientePecaExistsSpec : IDomainSpecification<NotaEntrada>
    {
        private readonly IPecaRepository _pecaRepository;
        private readonly NotaEntradaItens _item;

        public NotaEntradaClientePecaExistsSpec(IPecaRepository pecaRepository, NotaEntradaItens item)
        {
            _pecaRepository = pecaRepository;
            _item = item;
        }

        public bool IsSatisfiedBy(NotaEntrada entity)
        {
            if (_item == null) return true;

            return _pecaRepository.GetByCodigo(_item.Codigo) != null;
        }
    }
}

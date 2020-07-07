using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Producao.Domain.Pecas.Interfaces;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaEntradaFornecedorPecaUnidadeCompativelSpec : IDomainSpecification<NotaEntrada>
    {
        private readonly IPecaRepository _pecaRepository;
        private readonly NotaEntradaItens _item;
        private readonly Fornecedor _fornecedor;

        public NotaEntradaFornecedorPecaUnidadeCompativelSpec(IPecaRepository pecaRepository, NotaEntradaItens item, Fornecedor fornecedor)
        {
            _pecaRepository = pecaRepository;
            _item = item;
            _fornecedor = fornecedor;
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

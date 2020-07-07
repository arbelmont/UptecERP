using Definitiva.Shared.Domain.DomainValidator;
using System.Linq;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaEntradaItensCoincidentesSpec : IDomainSpecification<NotaEntrada>
    {
        private readonly NotaEntradaItens _itemCliente;

        public NotaEntradaItensCoincidentesSpec(NotaEntradaItens item)
        {
            _itemCliente = item;
        }

        public bool IsSatisfiedBy(NotaEntrada notaEntradaFornecedor)
        {
            var count = notaEntradaFornecedor.Itens.Count(i => i.CodigoCliente == _itemCliente.Codigo && 
                i.Unidade == _itemCliente.Unidade && i.Quantidade == _itemCliente.Quantidade);

            return count > 0;
        }
    }
}

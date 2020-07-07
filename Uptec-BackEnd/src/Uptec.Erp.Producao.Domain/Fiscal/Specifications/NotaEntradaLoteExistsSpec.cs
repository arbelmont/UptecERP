using Definitiva.Shared.Domain.DomainValidator;
using System.Collections.Generic;
using System.Linq;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Lotes.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaEntradaLoteExistsSpec : IDomainSpecification<NotaEntrada>
    {
        private readonly List<Lote> _lotes;

        public NotaEntradaLoteExistsSpec(List<Lote> lotes)
        {
            _lotes = lotes;
        }

        public bool IsSatisfiedBy(NotaEntrada notaFornecedor)
        {
            return _lotes.Count() > 0;
        }
    }
}

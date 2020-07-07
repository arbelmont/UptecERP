using Definitiva.Shared.Domain.DomainValidator;
using System.Collections.Generic;
using System.Linq;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Lotes.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaEntradaLoteEhCoberturaSpec : IDomainSpecification<NotaEntrada>
    {
        private readonly Lote _lote;

        public NotaEntradaLoteEhCoberturaSpec(Lote lote)
        {
            _lote = lote;
        }

        public bool IsSatisfiedBy(NotaEntrada notaFornecedor)
        {
            return _lote.EhCobertura;
        }
    }
}

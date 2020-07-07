using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaSaidaLotesHasCoberturaSpec : IDomainSpecification<NotaSaida>
    {
        private readonly NotaSaidaItens _item;
        private readonly ILoteRepository _loteRepository;

        public NotaSaidaLotesHasCoberturaSpec(NotaSaidaItens item, ILoteRepository loteRepository)
        {
            _item = item;
            _loteRepository = loteRepository;
        }

        public bool IsSatisfiedBy(NotaSaida entity)
        {
            var lote = _loteRepository.GetByNumero(_item.LoteNumero);

            if (lote == null) return true;

            if (lote.FornecedorId != null && lote.EhCobertura)
                return lote.ClienteId != null && lote.NotaFiscalCobertura != null;

            return lote.ClienteId != null;
        }
    }
}

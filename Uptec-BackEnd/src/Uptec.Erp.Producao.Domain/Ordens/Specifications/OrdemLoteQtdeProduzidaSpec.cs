using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Ordens.Models;

namespace Uptec.Erp.Producao.Domain.Ordens.Specifications
{
    class OrdemLoteQtdeProduzidaSpec : IDomainSpecification<Ordem>
    {
        private readonly OrdemLote _ordemLote;

        public OrdemLoteQtdeProduzidaSpec(OrdemLote ordemLote)
        {
            _ordemLote = ordemLote;
        }

        public bool IsSatisfiedBy(Ordem ordem)
        {
            return _ordemLote.QtdeProduzida > 0 && _ordemLote.QtdeProduzida <= _ordemLote.Qtde;
        }
    }
}

using System;
using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Ordens.Models;

namespace Uptec.Erp.Producao.Domain.Ordens.Specifications
{
    class OrdemLoteValidadeSpec : IDomainSpecification<Ordem>
    {
        private readonly OrdemLote _ordemLote;

        public OrdemLoteValidadeSpec(OrdemLote ordemLote)
        {
            _ordemLote = ordemLote;
        }

        public bool IsSatisfiedBy(Ordem ordem)
        {
            if(_ordemLote.Validade == null)
                return false;

            return _ordemLote.Validade.Value > DateTime.Now;
        }
    }
}

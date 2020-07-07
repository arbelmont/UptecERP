using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Integracao.Receita;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    public class CabecalhoNfeChaveExistsSpec : IDomainSpecification<CabecalhoNfe>
    {
        private readonly ICabecalhoNfeRepository _cabecalhoNfeRepository;
        private readonly DomainOperation _domainOperation;

        public CabecalhoNfeChaveExistsSpec(ICabecalhoNfeRepository cabecalhoNfeRepository, DomainOperation domainOperation)
        {
            _cabecalhoNfeRepository = cabecalhoNfeRepository;
            _domainOperation = domainOperation;
        }

        public bool IsSatisfiedBy(CabecalhoNfe entity)
        {
            var cabecalhoNfe = _cabecalhoNfeRepository.ObterPorChave(entity.ChaveNfe);

            if (_domainOperation == DomainOperation.Add)
                return cabecalhoNfe == null;

            return cabecalhoNfe == null || cabecalhoNfe.Id == entity.Id;
        }
    }
}

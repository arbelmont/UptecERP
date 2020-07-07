using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Pecas.Interfaces;
using Uptec.Erp.Producao.Domain.Pecas.Models;

namespace Uptec.Erp.Producao.Domain.Pecas.Specifications
{
    public class PecaCodigoUnicoSpec : IDomainSpecification<Peca>
    {
        private readonly IPecaRepository _pecaRepository;
        private readonly DomainOperation _domainOperation;

        public PecaCodigoUnicoSpec(IPecaRepository pecaRepository, DomainOperation domainOperation)
        {
            _pecaRepository = pecaRepository;
            _domainOperation = domainOperation;
        }

        public bool IsSatisfiedBy(Peca entity)
        {
            var peca = _pecaRepository.GetByCodigo(entity.Codigo);

            if (_domainOperation == DomainOperation.Add)
                return peca == null;

            return peca == null || peca.Id == entity.Id;
        }
    }
}

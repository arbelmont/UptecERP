using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Transportadoras.Interfaces;
using Uptec.Erp.Producao.Domain.Transportadoras.Models;

namespace Uptec.Erp.Producao.Domain.Transportadoras.Specifications
{
    public class TransportadoraCnpjUnicoSpec : IDomainSpecification<Transportadora>
    {
        private readonly ITransportadoraRepository _transportadoraRepository;
        private readonly DomainOperation _domainOperation;

        public TransportadoraCnpjUnicoSpec(ITransportadoraRepository transportadoraRepository, DomainOperation domainOperation)
        {
            _transportadoraRepository = transportadoraRepository;
            _domainOperation = domainOperation;
        }

        public bool IsSatisfiedBy(Transportadora entity)
        {
            var transportadora = _transportadoraRepository.GetByCnpj(entity.Cnpj.Numero);

            if (_domainOperation == DomainOperation.Add)
                return transportadora == null;

            return transportadora == null || transportadora.Id == entity.Id;
        }
    }
}
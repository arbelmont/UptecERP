using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Transportadoras.Interfaces;
using Uptec.Erp.Producao.Domain.Transportadoras.Models;
using Uptec.Erp.Producao.Domain.Transportadoras.Specifications;

namespace Uptec.Erp.Producao.Domain.Transportadoras.Validations
{
    public class TransportadoraCanUpdateValidation : DomainValidator<Transportadora>
    {
        public TransportadoraCanUpdateValidation(ITransportadoraRepository transportadoraRepository)
        {
            Add(new TransportadoraCnpjUnicoSpec(transportadoraRepository, DomainOperation.Update), "Cnpj já cadastrado." );
        }
    }
}
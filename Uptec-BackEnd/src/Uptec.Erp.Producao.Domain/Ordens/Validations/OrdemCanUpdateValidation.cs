using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Ordens.Interfaces;
using Uptec.Erp.Producao.Domain.Ordens.Models;

namespace Uptec.Erp.Producao.Domain.Ordens.Validations
{
    public class OrdemCanUpdateValidation : DomainValidator<Ordem>
    {
        public OrdemCanUpdateValidation(IOrdemRepository ordemRepository)
        {
            //TODO (criado automaticamente via gerador de codigo) Implemente sua lógica aqui e remova o exemplo abaixo
            // Add(new TransportadoraCnpjUnicoSpec(transportadoraRepository, DomainOperation.Add), "Cnpj já cadastrado." );
        }
    }
}
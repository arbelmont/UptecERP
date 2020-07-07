using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Producao.Domain.Fiscal.Validations
{
    public class NotaEntradaCanUpdateValidation : DomainValidator<NotaEntrada>
    {
        public NotaEntradaCanUpdateValidation(INotaEntradaRepository notaEntradaRepository)
        {
            //TODO (criado automaticamente via gerador de codigo) Implemente sua lógica aqui e remova o exemplo abaixo
            // Add(new TransportadoraCnpjUnicoSpec(transportadoraRepository, DomainOperation.Add), "Cnpj já cadastrado." );
        }
    }
}
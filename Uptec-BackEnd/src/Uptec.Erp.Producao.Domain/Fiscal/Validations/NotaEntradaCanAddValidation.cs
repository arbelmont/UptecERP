using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Specifications;

namespace Uptec.Erp.Producao.Domain.Fiscal.Validations
{
    public class NotaEntradaCanAddValidation : DomainValidator<NotaEntrada>
    {
        public NotaEntradaCanAddValidation(INotaEntradaRepository notaEntradaRepository)
        {
            Add(new NotaEntradaNumeroUnicoSpec(notaEntradaRepository, DomainOperation.Add), "Nota já cadastrada");
        }
    }
}
using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Specifications;

namespace Uptec.Erp.Producao.Domain.Fiscal.Validations
{
    public class NotaEntradaCanDeleteValidation : DomainValidator<NotaEntrada>
    {
        public NotaEntradaCanDeleteValidation()
        {
            Add(new NotaEntradaNotNullSpec(), "Nota Fiscal inexistente.");
            Add(new NotaEntradaStatusConciliarSpec(), "Somente notas não conciliadas podem ser excluidas.");
        }
    }
}
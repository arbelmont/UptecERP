using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Specifications;

namespace Uptec.Erp.Producao.Domain.Fiscal.Validations
{
    public class NotaEntradaCanUpdateTipoEmissorValidation : DomainValidator<NotaEntrada>
    {
        public NotaEntradaCanUpdateTipoEmissorValidation()
        {
            Add(new NotaEntradaNotNullSpec(), "Nota Fiscal inexistente.");
            Add(new NotaEntradaNotCliForSpec(), "Impossível alterar o tipo de emissor dessa nota.");
        }
    }
}
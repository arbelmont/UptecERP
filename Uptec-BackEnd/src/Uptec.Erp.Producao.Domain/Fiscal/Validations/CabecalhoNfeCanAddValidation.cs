using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Integracao.Receita;
using Uptec.Erp.Producao.Domain.Fiscal.Specifications;

namespace Uptec.Erp.Producao.Domain.Fiscal.Validations
{
    public class CabecalhoNfeCanAddValidation : DomainValidator<CabecalhoNfe>
    {
        public CabecalhoNfeCanAddValidation(ICabecalhoNfeRepository cabecalhoNfeRepository)
        {
            Add(new CabecalhoNfeChaveExistsSpec(cabecalhoNfeRepository, DomainOperation.Add), "Cabeçalho de Nota Fiscal já gravado.");
        }
    }
}

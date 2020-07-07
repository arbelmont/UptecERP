using Definitiva.Shared.Domain.DomainValidator;
using Definitiva.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Integracao.Receita;
using Uptec.Erp.Producao.Domain.Fiscal.Specifications;

namespace Uptec.Erp.Producao.Domain.Fiscal.Validations
{
    class CabecalhoNfeCanDeleteValidation : DomainValidator<CabecalhoNfe>
    {
        public CabecalhoNfeCanDeleteValidation(ICabecalhoNfeRepository cabecalhoNfeRepository)
        {
            Add(new CabecalhoNfeChaveExistsSpec(cabecalhoNfeRepository, DomainOperation.Update), "Cabeçalho de Nota Fiscal não encontrado");
        }
    }
}

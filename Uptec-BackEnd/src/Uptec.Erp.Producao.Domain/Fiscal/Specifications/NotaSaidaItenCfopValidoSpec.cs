using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Cfop;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    public class NotaSaidaItenCfopValidoSpec : IDomainSpecification<NotaSaida>
    {
        private NotaSaidaItens _item;
        public NotaSaidaItenCfopValidoSpec(NotaSaidaItens item)
        {
            _item = item;
        }
        public bool IsSatisfiedBy(NotaSaida entity)
        {
            if(entity.Tipo != TipoNotaSaida.PecaAvulsa)
                return CfopUptec.CfopsSaida.Contains(_item.Cfop);
            
            int cfop = 0;

            if(!int.TryParse(_item.Cfop, out cfop))
                return false;
            
            return (cfop >= 5000 && cfop <= 6999);
        }
    }
}
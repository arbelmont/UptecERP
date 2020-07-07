using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Ordens.Models;
using Uptec.Erp.Producao.Domain.Ordens.Specifications;

namespace Uptec.Erp.Producao.Domain.Ordens.Validations
{
    public class OrdemCanFinalizarValidation : DomainValidator<Ordem>
    {
        public OrdemCanFinalizarValidation(Ordem ordem)
        {
            foreach (var item in ordem.OrdemLotes)
            {
                Add(new OrdemLoteQtdeProduzidaSpec(item), $"A Quantidade Produzida para o Lote: ${item.LoteNumero} é inválida.");
                Add(new OrdemLoteValidadeSpec(item), $"A Validade para o Lote: ${item.LoteNumero} é inválida.");
            }
        }
    }
}
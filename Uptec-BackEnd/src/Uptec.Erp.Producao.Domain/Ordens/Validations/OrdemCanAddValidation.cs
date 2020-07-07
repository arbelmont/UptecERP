using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;
using Uptec.Erp.Producao.Domain.Ordens.Models;
using Uptec.Erp.Producao.Domain.Ordens.Specifications;

namespace Uptec.Erp.Producao.Domain.Ordens.Validations
{
    public class OrdemCanAddValidation : DomainValidator<Ordem>
    {
        public OrdemCanAddValidation(Ordem ordem, 
            ILoteRepository loteRepository)
        {
            foreach (var item in ordem.OrdemLotes)
            {
                Add(new OrdemLoteExistsSpec(loteRepository, item.LoteId), $"Lote Número: {item.LoteNumero} não existe");
                Add(new OrdemLoteTipoPecaSpec(loteRepository, item.LoteId), $"Impossível gerar ordem de produção para o Lote Número: {item.LoteNumero}, lote refere-se a uma embalagem");
                Add(new OrdemLoteQtdeSuficienteSpec(loteRepository, item.LoteId, item.Qtde), $"Quantidade insuficiente no Lote Número: {item.LoteNumero}");
            }
        }
    }
}
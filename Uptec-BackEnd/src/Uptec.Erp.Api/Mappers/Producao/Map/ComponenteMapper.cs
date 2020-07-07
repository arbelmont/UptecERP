using ExpressMapper;
using Uptec.Erp.Api.ViewModels.Producao.Componente;
using Uptec.Erp.Producao.Domain.Componentes.Models;

namespace Uptec.Erp.Api.Mappers.Producao.Map
{
    public class ComponenteMapper
    {
        public ComponenteMapper()
        {
            Mapper.Register<Componente, ComponenteViewModel>();
            Mapper.Register<ComponenteViewModel, Componente>()
                .Instantiate(src => new Componente(src.Id, src.Codigo, src.Descricao, src.Unidade, src.Preco, src.Ncm, src.Quantidade, src.QuantidadeMinima));

            Mapper.Register<ComponenteMovimento, ComponenteMovimentoViewModel>();
            Mapper.Register<ComponenteMovimentoViewModel, ComponenteMovimento>()
                .Instantiate(src => new ComponenteMovimento(src.Id, src.ComponenteId, src.Quantidade, src.TipoMovimento, src.PrecoUnitario, src.NotaFiscal, src.Historico));
        }
    }
}


using ExpressMapper;
using ExpressMapper.Extensions;
using System.Collections.Generic;
using System.Linq;
using Uptec.Erp.Api.ViewModels.Producao.NotasSaida;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Dtos;
using static Uptec.Erp.Producao.Domain.Fiscal.Models.NotaSaida;

namespace Uptec.Erp.Api.Mappers.Producao.Map
{
    public class NotaSaidaMapper
    {
        public NotaSaidaMapper()
        {
            Mapper.Register<NotaSaida, NotaSaidaViewModel>();
            Mapper.Register<NotaSaidaItens, NotaSaidaItensViewModel>();

            Mapper.Register<NotaSaidaViewModel, NotaSaida>()
                .Instantiate(src => NotaSaidaFactory.NotaSaidaCompleta(src.Id, src.NumeroNota, src.Data, src.ValorBaseCalculo, 
                    src.ValorIcms, src.ValorTotalProdutos, src.ValorFrete, src.ValorSeguro, src.ValorDesconto, src.ValorOutrasDespesas, 
                    src.ValorIpi, src.ValorTotalNota, src.TipoDestinatario, src.ClienteId.Value, src.FornecedorId.Value,
                    src.Itens.ToList().Map<List<NotaSaidaItensViewModel>, List<NotaSaidaItens>>()));

            Mapper.Register<NotaSaidaItensViewModel, NotaSaidaItens>()
                .Instantiate(src => new NotaSaidaItens(src.Id, src.Codigo, src.Descricao, src.Cfop, src.Ncm,
                             src.Unidade, src.ValorUnitario, src.Quantidade, src.OrdemNumero.Value, src.LoteNumero,
                             src.LoteSequencia, src.NotaSaidaId));

            Mapper.Register<NotaSaidaAddViewModel, NotaSaidaAddDto>();
            Mapper.Register<NotaSaidaOrdemItensViewModel, NotaSaidaOrdemItensDto>();
            Mapper.Register<NotaSaidaLoteItensViewModel, NotaSaidaLoteItensDto>();
        }
    }
}

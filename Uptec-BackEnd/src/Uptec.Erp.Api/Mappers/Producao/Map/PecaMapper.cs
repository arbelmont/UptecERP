using ExpressMapper;
using ExpressMapper.Extensions;
using System.Collections.Generic;
using Uptec.Erp.Api.ViewModels.Producao.Pecas;
using Uptec.Erp.Producao.Domain.Pecas.Models;

namespace Uptec.Erp.Api.Mappers.Producao.Map
{
    public class PecaMapper
    {
        public PecaMapper()
        {
            Mapper.Register<PecaViewModel, Peca>()
                .Instantiate(src => new Peca(src.Id, src.Codigo, src.CodigoSaida, src.Descricao, src.Unidade,
                                             src.Tipo, src.Preco, src.PrecoSaida, src.Ncm, src.Revisao,
                                             src.ClienteId,
                                             src.Componentes.Map<List<PecaComponenteViewModel>, List<PecaComponente>>(),
                                             src.CodigosFornecedor.Map<List<PecaFornecedorCodigoViewModel>, List<PecaFornecedorCodigo>>()
                                             ));

            Mapper.Register<PecaComponenteViewModel, PecaComponente>()
                .Instantiate(src => new PecaComponente(src.PecaId, src.ComponenteId, src.Quantidade));

            Mapper.Register<PecaFornecedorCodigoViewModel, PecaFornecedorCodigo>()
                .Instantiate(src => new PecaFornecedorCodigo(src.PecaId, src.FornecedorId, src.FornecedorCodigo));

            Mapper.Register<Peca, PecaViewModel>();

            Mapper.Register<PecaComponente, PecaComponenteViewModel>();

            Mapper.Register<PecaFornecedorCodigo, PecaFornecedorCodigoViewModel>();
        }
    }
}

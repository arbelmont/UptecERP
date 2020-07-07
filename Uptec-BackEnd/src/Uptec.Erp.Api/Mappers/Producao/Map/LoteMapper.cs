using ExpressMapper;
using System;
using Uptec.Erp.Api.ViewModels.Producao.Lotes;
using Uptec.Erp.Producao.Domain.Lotes.Models;

namespace Uptec.Erp.Api.Mappers.Producao.Map
{
    public class LoteMapper
    {
        public LoteMapper()
        {
            Mapper.Register<LoteViewModel, Lote>()
                .Instantiate(src => new Lote(src.PecaId, src.Data, src.PecaId, src.TipoPeca, src.Quantidade, src.PrecoEntrada, src.CfopEntrada, src.NotaFiscal, src.DataFabricacao, src.DataValidade, src.Localizacao, src.QtdeConcilia));

            Mapper.Register<LoteAddViewModel, Lote>()
                .Instantiate(src => new Lote(Guid.NewGuid(), DateTime.Now, src.PecaId, src.TipoPeca, src.Quantidade, src.PrecoEntrada, src.CfopEntrada, src.NotaFiscal, src.DataFabricacao, src.DataValidade, src.Localizacao, src.QtdeConcilia));

            Mapper.Register<LoteMovimentoViewModel, LoteMovimento>()
                .Instantiate(src => new LoteMovimento(src.Id, src.Data, src.LoteId, src.LoteSequencia, src.Quantidade, src.PrecoUnitario, src.NotaFiscal, src.TipoMovimento, src.Historico));

            Mapper.Register<LoteAddMovimentoViewModel, LoteMovimento>()
                .Instantiate(src => new LoteMovimento(Guid.NewGuid(), DateTime.Now, src.LoteId, src.LoteSequencia, src.Quantidade, src.PrecoUnitario, src.NotaFiscal, src.TipoMovimento, src.Historico));

            Mapper.Register<Lote, LoteViewModel>();

            Mapper.Register<LoteMovimento, LoteMovimentoViewModel>();

            Mapper.Register<LoteDadosSaida, LoteDadosSaidaViewModel>();
            Mapper.Register<LoteSaldo, LoteSaldoViewModel>();
        }
    }
}

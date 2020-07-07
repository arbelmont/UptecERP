using ExpressMapper;
using ExpressMapper.Extensions;
using System;
using System.Collections.Generic;
using Uptec.Erp.Api.ViewModels.Producao.Ordem;
using Uptec.Erp.Producao.Domain.Ordens.Models;

namespace Uptec.Erp.Api.Mappers.Producao.Map
{
    public class OrdemMapper
    {
        public OrdemMapper()
        {
            Mapper.Register<Ordem, OrdemViewModel>();
            Mapper.Register<Ordem, OrdemNfeSaidaViewModel>();
            Mapper.Register<OrdemLote, OrdemLoteViewModel>();
            Mapper.Register<OrdemLote, OrdemLoteNfeSaidaViewModel>();
            Mapper.Register<LinhaProducao, LinhaProducaoViewModel>();

            Mapper.Register<OrdemAddViewModel, Ordem>()
                .Instantiate(src => new Ordem(Guid.NewGuid(),
                                              src.QtdeTotal,
                                              src.OrdemLotes.Map<List<OrdemLoteAddViewModel>, List<OrdemLote>>()));

            Mapper.Register<OrdemLoteAddViewModel, OrdemLote>()
                .Instantiate(src => new OrdemLote(Guid.NewGuid(), src.LoteNumero, src.LoteSequencia, src.Qtde, src.LoteId));

            Mapper.Register<OrdemLoteFinalizarViewModel, OrdemLote>()
                .Instantiate(src => new OrdemLote(src.Id, src.LoteNumero, src.LoteSequencia, src.Qtde, src.LoteId))
                .After((src, dst) => {
                    dst.SetQtdeProduzida(src.QtdeProduzida);
                    dst.SetMotivoExpedicao(src.MotivoExpedicao);
                    dst.SetValidade(src.Validade);
                });
        }
    }
}

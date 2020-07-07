using ExpressMapper;
using ExpressMapper.Extensions;
using System.Collections.Generic;
using Uptec.Erp.Api.ViewModels.Producao.NotasEntrada;
using Uptec.Erp.Producao.Domain.Fiscal.Models;

namespace Uptec.Erp.Api.Mappers.Producao.Map
{
    public class NotaEntradaMapper
    {
        public NotaEntradaMapper()
        {
            Mapper.Register<NotaEntrada, NotaEntradaViewModel>()
                .Member(dst => dst.CnpjEmissor, src => src.CnpjEmissor.NumeroFormatadoSemValidacao)
                .Member(dst => dst.EmailEmissor, src => src.EmailEmissor.EnderecoEmail);
            Mapper.Register<NotaEntradaItens, NotaEntradaItensViewModel>();

            Mapper.Register<NotaEntradaViewModel, NotaEntrada>()
                .Instantiate(src => new NotaEntrada(src.Id, src.NumeroNota, src.Data, src.Valor,
                                                    src.Cfop, src.CnpjEmissor, src.NomeEmissor, src.EmailEmissor,
                                                    src.Itens.Map<List<NotaEntradaItensViewModel>, List<NotaEntradaItens>>()));

            Mapper.Register<NotaEntradaItensViewModel, NotaEntradaItens>()
                .Instantiate(src => new NotaEntradaItens(src.Id, src.Codigo, src.Descricao, src.Cfop, src.Unidade, src.PrecoUnitario,
                                src.Quantidade, src.NotaEntradaId));
        }
    }
}

using System.Linq;
using System.Xml.Linq;
using Uptec.Erp.Api.Controllers.v1.Fiscal.NotasEntrada.Domain.Interfaces;
using Uptec.Erp.Api.ViewModels.Producao.NotasEntrada;
using Uptec.Erp.Shared.Domain.Enums;
using Definitiva.Shared.Infra.Support.Helpers;
using System;
using Definitiva.Shared.Domain.Services;
using Definitiva.Shared.Domain.Bus;
using System.Collections.Generic;

namespace Uptec.Erp.Api.Controllers.v1.Fiscal.NotasEntrada.Domain.Services
{
    public class NotaEntradaAppService : BaseService, INotaEntradaAppService
    {
        private readonly NotaEntradaViewModel _notaEntradaViewModel;
        private XElement _xmlNfe;


        private XNamespace _xmlNs;
        public NotaEntradaAppService(IBus bus) :base(bus)
        {
            _notaEntradaViewModel = new NotaEntradaViewModel();
        }
        public NotaEntradaViewModel EntrairDadosNotaEntrada(string arquivoXmlNfe)
        {
            _xmlNfe = XElement.Parse(arquivoXmlNfe);
            if (_xmlNfe == null)
                NotifyError("O arquivo XML da Nota Fiscal de Entrada está inconsistente ou inválido");

            _xmlNs = _xmlNfe.GetDefaultNamespace();

            ObterItensNfe();
            ObterEmitenteNfe();
            ObterDadosNfe();
            ObterTotaisNfe();

            _notaEntradaViewModel.Cfop = _notaEntradaViewModel.Itens.FirstOrDefault().Cfop;

            return _notaEntradaViewModel;
        }

        private void ObterDadosNfe()
        {
            var nfe = _xmlNfe.Element(Elemento("NFe")).Element(Elemento("infNFe")).Element(Elemento("ide"));

            DateTime.TryParse(nfe.Element(Elemento("dhEmi")).Value, out var dataEmissao);
            _notaEntradaViewModel.Data = dataEmissao;

            _notaEntradaViewModel.NumeroNota = nfe.Element(Elemento("nNF")).Value;
        }

        private void ObterTotaisNfe()
        {
            var totaisNfe = _xmlNfe.Element(Elemento("NFe")).Element(Elemento("infNFe")).Element(Elemento("total")).Element(Elemento("ICMSTot"));

            _notaEntradaViewModel.Valor = totaisNfe.Element(Elemento("vNF")).Value.ToDecimal();
        }

        private void ObterEmitenteNfe()
        {
            var emitenteNfe = _xmlNfe.Element(Elemento("NFe")).Element(Elemento("infNFe")).Element(Elemento("emit"));

            _notaEntradaViewModel.CnpjEmissor = emitenteNfe.Element(Elemento("CNPJ")).Value;
            _notaEntradaViewModel.NomeEmissor = emitenteNfe.Element(Elemento("xNome")).Value;
            //_notaEntradaViewModel.TipoEmissor = ??
            // _notaEntradaViewModel.EmailEmissor = ??
        }

        private void ObterItensNfe()
        {
            var itensNfe = _xmlNfe.Element(Elemento("NFe")).Element(Elemento("infNFe")).Elements(Elemento("det")).Elements(Elemento("prod"));

            foreach (var item in itensNfe)
            {
                var itemNfe = new NotaEntradaItensViewModel()
                {
                    Cfop = item.Element(Elemento("CFOP")).Value,
                    Descricao = item.Element(Elemento("xProd")).Value,
                    Unidade = item.Element(Elemento("uCom")).Value.ToEnum<UnidadeMedida>(),
                    Quantidade = item.Element(Elemento("qCom")).Value.ToDecimal(),
                    Codigo = item.Element(Elemento("cProd")).Value,
                    PrecoUnitario = item.Element(Elemento("vUnCom")).Value.ToDecimal(),
                    PrecoTotal = item.Element(Elemento("vProd")).Value.ToDecimal(),
                };

                _notaEntradaViewModel.Itens.Add(itemNfe);
            }

            var itensAgrupados = _notaEntradaViewModel.Itens.GroupBy(x => x.Codigo).Select(x => x.First());

            var novaLista = new List<NotaEntradaItensViewModel>();

            foreach (var i in itensAgrupados)
            {
                var itemNfe = new NotaEntradaItensViewModel()
                {
                    Cfop = i.Cfop,
                    Descricao = i.Descricao,
                    Unidade = i.Unidade,
                    Quantidade = _notaEntradaViewModel.Itens.Where(x => x.Codigo == i.Codigo).Sum(x => x.Quantidade),
                    Codigo = i.Codigo,
                    PrecoUnitario = i.PrecoUnitario,
                    PrecoTotal = (i.Quantidade * i.PrecoUnitario)
                };
                novaLista.Add(itemNfe);
            }

            _notaEntradaViewModel.Itens = novaLista;
        }

        private XName Elemento(string element)
        {
            return (_xmlNs + element);
        }
    }
}

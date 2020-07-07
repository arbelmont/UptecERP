using System;
using System.Collections.Generic;
using System.Linq;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Infra.Integracao.NFe.Models;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Enums.NFe;

namespace Uptec.Erp.Producao.Infra.Integracao.NFe.Mappers
{
    public static class NotaFiscalItensMapper
    {
        public static ICollection<NotaFiscalItem> ToIntegracao(this ICollection<NotaSaidaItens> itens,
            TipoNotaSaida tipoNota )
        {
            var notaItens = itens.OrderBy(i => i.LoteNumero + i.LoteSequencia).ThenBy(i => (int)i.TipoItem).ToList().ToList();
            var notaFiscalItens = new List<NotaFiscalItem>();

            for (short i = 0; i < itens.Count; i++)
            {
                var notaFiscalItem = new NotaFiscalItem();

                notaFiscalItem.numero_item = Convert.ToInt16(i + 1);
                notaFiscalItem.codigo_produto = notaItens[i].Codigo;
                notaFiscalItem.descricao = notaItens[i].Descricao;
                notaFiscalItem.cfop = notaItens[i].Cfop;
                notaFiscalItem.unidade_comercial = notaItens[i].Unidade.ToString();
                notaFiscalItem.quantidade_comercial = notaItens[i].Quantidade;
                notaFiscalItem.valor_unitario_comercial = notaItens[i].ValorUnitario;
                notaFiscalItem.valor_unitario_tributavel = notaItens[i].ValorUnitario;
                notaFiscalItem.unidade_tributavel = notaItens[i].Unidade.ToString();
                notaFiscalItem.codigo_ncm = notaItens[i].Ncm;
                notaFiscalItem.quantidade_tributavel = notaItens[i].Quantidade;
                notaFiscalItem.valor_bruto = notaItens[i].ValorTotal;

                notaFiscalItem.icms_origem = IcmsOrigemNfe.Nacional;
                notaFiscalItem.icms_situacao_tributaria = notaItens[i].IcmsSituacaoTributaria;

                if (notaFiscalItem.icms_situacao_tributaria == IcmsSituacaoTributariaNfe.TributadaIntegralmente) 
                {
                    notaFiscalItem.ipi_situacao_tributaria = 51;
                    notaFiscalItem.ipi_codigo_enquadramento_legal = 109;

                    notaFiscalItem.icms_aliquota = notaItens[i].AliquotaIcms;
                    notaFiscalItem.icms_base_calculo = notaItens[i].ValorBaseCalculo;
                    notaFiscalItem.icms_modalidade_base_calculo = 0;
                    notaFiscalItem.icms_valor = notaItens[i].ValorIcms;

                    notaFiscalItem.pis_aliquota_porcentual = notaItens[i].AliquotaPis;
                    notaFiscalItem.pis_base_calculo = notaItens[i].ValorBaseCalculo;
                    notaFiscalItem.pis_valor = notaItens[i].ValorPis;

                    notaFiscalItem.cofins_aliquota_porcentual = notaItens[i].AliquotaCofins;
                    notaFiscalItem.cofins_base_calculo = notaItens[i].ValorBaseCalculo;
                    notaFiscalItem.cofins_valor = notaItens[i].ValorCofins;
                }
                else
                {
                    notaFiscalItem.ipi_situacao_tributaria = 53;
                    notaFiscalItem.ipi_codigo_enquadramento_legal = 999;
                }

                if( tipoNota == TipoNotaSaida.PecaAvulsa)
                    notaFiscalItem.ipi_situacao_tributaria = 99;

                notaFiscalItem.pis_situacao_tributaria = notaItens[i].PisSituacaoTributaria;        //TODO ARBTEC
                notaFiscalItem.cofins_situacao_tributaria = notaItens[i].CofinsSituacaoTributaria;    //TODO ARBTEC
                notaFiscalItem.ipi_valor = notaItens[i].ValorIpi;

                notaFiscalItens.Add(notaFiscalItem);
            }

            return notaFiscalItens;
        }
    }
}

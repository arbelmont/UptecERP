using ExpressMapper;
using System;
using Uptec.Erp.Api.ViewModels.Producao.NotasEntrada.SEFAZ;
using Uptec.Erp.Api.ViewModels.Producao.NotasSaida;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Dtos.Integracao.Receita;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Integracao.Receita;
using Uptec.Erp.Shared.Domain.Models.lntegracaoNFe.ConsultaNFe;

namespace Uptec.Erp.Api.Mappers.Producao.Map
{
    public class NotaFiscalIntegracaoMapper
    {
        public NotaFiscalIntegracaoMapper()
        {
            Mapper.Register<ConsultaNfeIntegracao, NotaSaidaStatusProcessamentoViewModel>()
                .Member(dst => dst.Numero, src => src.Numer);

            Mapper.Register<NotaSaidaStatusProcessamentoViewModel, ConsultaNfeIntegracao>()
                .Member(dst => dst.Numer, src => src.Numero);

            
            Mapper.Register<CabecalhoNfeViewModel, CabecalhoNfe>()
                .Instantiate(src => new CabecalhoNfe(src.Nome_emitente, src.Documento_emitente,
                                                     src.Chave_nfe, ConveterParaDecimal(src.Valor_total), src.Data_emissao,
                                                     src.Situacao, src.Manifestacao_destinatario, src.Justificativa, src.Nfe_completa,
                                                     src.Tipo_nfe, src.Versao, src.Digest_value,
                                                     src.Numero_carta_correcao, src.Carta_correcao,
                                                     src.Data_carta_correcao, src.Data_cancelamento,
                                                     src.Justificativa_cancelamento, null, null));

            Mapper.Register<CabecalhoNfeDto, CabecalhoNfe>()
                .Instantiate(src => new CabecalhoNfe(src.Nome_emitente, src.Documento_emitente,
                                                     src.Chave_nfe, ConveterParaDecimal(src.Valor_total), src.Data_emissao.Value,
                                                     src.Situacao.Value, src.Manifestacao_destinatario, src.Justificativa, src.Nfe_completa,
                                                     src.Tipo_nfe.Value, src.Versao, src.Digest_value,
                                                     src.Numero_carta_correcao, src.Carta_correcao,
                                                     src.Data_carta_correcao, src.Data_cancelamento,
                                                     src.Justificativa_cancelamento, null, null));

            Mapper.Register<CabecalhoNfeManifestacaoViewModel, CabecalhoNfeManifestacaoViewModelResult>();
                
        }
        private decimal ConveterParaDecimal(string valor)
        {
            if (valor is null)
                return -1;

            if (!decimal.TryParse(valor.Replace(".", ","), out var result))
                throw new ArgumentException("Foi retornado um 'valor_total' inválido na consulta de notas fiscais de fornecedores.");

            return result;
        }

    }
}

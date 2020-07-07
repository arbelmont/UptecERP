using System.Linq;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Infra.Integracao.NFe.Models;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Enums.NFe;
using Uptec.Erp.Shared.Domain.Models.Endereco;

namespace Uptec.Erp.Producao.Infra.Integracao.NFe.Mappers
{
    public static class NotaFiscalMapper
    {
        public static NotaFiscal ToIntegracao(this NotaSaida notaSaida)
        {
            var notaFiscal = new NotaFiscal
            {
                numero = int.Parse(notaSaida.NumeroNota),
                serie = 0,
                natureza_operacao = notaSaida.NaturezaOperacao,
                data_emissao = notaSaida.Data.ToString("yyyy-MM-dd"),
                tipo_documento = TipoDocumentoNfe.NotaFiscalSaida,
                local_destino = notaSaida.LocalDestino,
                finalidade_emissao = notaSaida.FinalidadeEmissao,
                consumidor_final = ConsumidorFinalNfe.Normal,
                presenca_comprador = PresencaCompradorNfe.NaoSeAplica,
                informacoes_adicionais_contribuinte = notaSaida.OutrasInformacoes,

                #region Tributos
                valor_frete = notaSaida.ValorFrete,
                valor_seguro = notaSaida.ValorSeguro,
                valor_total = notaSaida.ValorTotalNota,
                valor_produtos = notaSaida.ValorTotalProdutos,
                valor_desconto = notaSaida.ValorDesconto,
                icms_base_calculo = notaSaida.ValorBaseCalculo,
                icms_valor_total = notaSaida.ValorIcms,
                //valor_ipi = notaSaida.ValorIpi,
                valor_pis = notaSaida.ValorPis,
                valor_cofins = notaSaida.ValorCofins,
                valor_outras_despesas = notaSaida.ValorOutrasDespesas,
                modalidade_frete = notaSaida.ModalidadeFrete,
                #endregion

                #region Emitente
                nome_emitente = "Uptec Brasil Tratamento Superficies de Peças Ltda",
                cnpj_emitente = "08059371000150",                                               //Aqui (Dados da Daltão)
                inscricao_estadual_emitente = "239043867112",                                   //Aqui
                logradouro_emitente = "Rua Ádamo Zambelli",                                     //Aqui
                numero_emitente = "23",                                                         //Aqui
                bairro_emitente = "Calcárea",                                                    //Aqui
                municipio_emitente = "Caieiras",
                uf_emitente = "SP",                                                             //Aqui
                cep_emitente = "07723-000",                                                     //Aqui
                regime_tributario_emitente = RegimeTributarioEmitenteNfe.RegimeNormal,          //Aqui
                #endregion


                items = notaSaida.Itens.ToIntegracao(notaSaida.Tipo)

            };

            #region TransportadoraFrete

            if(notaSaida.TransportadoraId != null)
            {
                var enderecoTransporadora = (Endereco)notaSaida.EnderecoTransportadora;
                notaFiscal.cnpj_transportador = notaSaida.Transportadora.Cnpj.Numero;
                notaFiscal.nome_transportador = notaSaida.Transportadora.NomeFantasia;
                notaFiscal.inscricao_estadual_transportador = notaSaida.Transportadora.InscricaoEstadual;
                notaFiscal.endereco_transportador = $"{enderecoTransporadora.Logradouro}, {enderecoTransporadora.Numero} {enderecoTransporadora.Complemento} {enderecoTransporadora.Bairro}";
                notaFiscal.municipio_transportador = enderecoTransporadora.Cidade.Nome;
                notaFiscal.uf_transportador = enderecoTransporadora.Estado.Sigla;
            }
            

            #endregion


            #region Destinatário

            var endereco = (Endereco)notaSaida.EnderecoDestinatario;
            if (notaSaida.TipoDestinatario == TipoDestinatario.Cliente)
            {
                notaFiscal.nome_destinatario = notaSaida.Cliente.RazaoSocial;
                notaFiscal.cnpj_destinatario = notaSaida.Cliente.Cnpj.Numero;
                notaFiscal.inscricao_estadual_destinatario = notaSaida.Cliente.InscricaoEstadual;
            }
            else
            {
                notaFiscal.nome_destinatario = notaSaida.Fornecedor.RazaoSocial;
                notaFiscal.cnpj_destinatario = notaSaida.Fornecedor.Cnpj.Numero;
                notaFiscal.inscricao_estadual_destinatario = notaSaida.Fornecedor.InscricaoEstadual;
            }

            notaFiscal.logradouro_destinatario = endereco.Logradouro;
            notaFiscal.numero_destinatario = endereco.Numero;
            notaFiscal.bairro_destinatario = endereco.Bairro;
            notaFiscal.municipio_destinatario = endereco.Cidade.Nome;
            notaFiscal.uf_destinatario = endereco.Estado.Sigla;
            notaFiscal.pais_destinatario = "Brasil";
            notaFiscal.cep_destinatario = endereco.Cep;
            notaFiscal.indicador_inscricao_estadual_destinatario = IndicadorInscricaoEstadualDestinatarioNfe.ContribuinteICMS;

            #endregion

            return notaFiscal;
        }
    }
}

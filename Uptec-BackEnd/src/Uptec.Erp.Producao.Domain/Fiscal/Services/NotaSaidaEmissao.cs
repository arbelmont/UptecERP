using Uptec.Erp.Shared.Domain.Models.Endereco;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Dtos;
using System.Collections.Generic;
using System;
using Uptec.Erp.Producao.Domain.Ordens.Interfaces;
using Uptec.Erp.Shared.Domain.Models.Impostos;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Producao.Domain.Ordens.Models;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;
using Uptec.Erp.Shared.Domain.Enums.NFe;
using Definitiva.Shared.Infra.Support.Helpers;
using System.Linq;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Shared.Domain.ValueObjects;
using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.Services;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Producao.Domain.Clientes.Interfaces;
using Uptec.Erp.Producao.Domain.Fornecedores.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Validations;
using System.Text;

namespace Uptec.Erp.Producao.Domain.Fiscal.Services
{
    public class NotaSaidaEmissao : BaseService, INotaSaidaEmissao
    {
        private readonly INotaSaidaRepository _notaSaidaRepository;
        private readonly IOrdemRepository _ordemRepository;
        private readonly ILoteRepository _loteRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly ILoteService _loteService;
         
        public AliquotaImpostos AliquotaImpostos { get; private set; }
        public NotaSaidaDestinatario Destinatario { get; private set; }

        public NotaSaidaEmissao(IBus bus,
                                INotaSaidaRepository fiscalRepository,
                                IOrdemRepository ordemRepository,
                                ILoteRepository loteRepository,
                                IClienteRepository clienteRepository,
                                IFornecedorRepository fornecedorRepository,
                                ILoteService loteService) : base(bus)
        {
            _notaSaidaRepository = fiscalRepository;
            _ordemRepository = ordemRepository;
            _loteRepository = loteRepository;
            _clienteRepository = clienteRepository;
            _fornecedorRepository = fornecedorRepository;
            _loteService = loteService;
        }

        public string GetOutrasInformacoes(NotaSaidaAddDto dto)
        {
            Destinatario = GenerateDestinatario(dto);

            AliquotaImpostos = GetAliquotaImpostos(Destinatario.Endereco.Estado.Sigla);

            NotaSaida nota;

            if (dto.TipoNota == TipoNotaSaida.Peca)
                nota = EmitirNotaParaPeca(dto, false);
            else if(dto.TipoNota == TipoNotaSaida.Embalagem)
                nota = EmitirNotaParaEmbalagem(dto, false);
            else if(dto.TipoNota == TipoNotaSaida.PecaAvulsa)
                nota = EmitirNotaAvulsa(dto, false);
            else
            {
                NotifyError("Tipo de nota não identificado");
                return null;
            }

            return nota.OutrasInformacoes;
        }

        public NotaSaida EmitirNota(NotaSaidaAddDto dto)
        {
            if (!dto.LoteItens.Any() && !dto.OrdemItens.Any())
            {
                NotifyError("Nenhum item identificado para emissão da nota");
                return null;
            }

            Destinatario = GenerateDestinatario(dto);

            if (Destinatario.Endereco == null) { NotifyError("Endereco não encontrado"); return null; }

            AliquotaImpostos = GetAliquotaImpostos(Destinatario.Endereco.Estado.Sigla);

            NotaSaida nota;

            if (dto.TipoNota == TipoNotaSaida.Peca)
                nota = EmitirNotaParaPeca(dto);
            else if(dto.TipoNota == TipoNotaSaida.Embalagem)
                nota = EmitirNotaParaEmbalagem(dto);
            else if(dto.TipoNota == TipoNotaSaida.PecaAvulsa)
                nota = EmitirNotaAvulsa(dto);
            else
            {
                NotifyError("Tipo de nota não identificado");
                return null;
            }

            if (!ValidateEntity(nota)) return null;

            if (!ValidateBusinessRules(new NotaSaidaCanAddValidation(nota, Destinatario.Cliente,
                Destinatario.Fornecedor, Destinatario.Endereco, _notaSaidaRepository, _loteRepository)
                .Validate(nota))) return null;

            return nota;
        }

        private NotaSaida EmitirNotaParaPeca(NotaSaidaAddDto dto, bool gravaDados = true)
        {
            var itens = new List<NotaSaidaItens>();

            var numeroNota = gravaDados ? _notaSaidaRepository.GetNotaSaidaSequence().ToString(): "0000";

            foreach (var item in dto.OrdemItens)
            {
                var ordemLote = _ordemRepository.GetOrdemLoteFullById(item.OrdemLoteId);
                if (ordemLote == null)
                {
                    NotifyError("Lote não identificado");
                    return null;
                }

                itens.AddRange(GenerateItens(ordemLote));
            }

            var nota = new NotaSaida(Guid.NewGuid(), dto.DestinatarioId, dto.EnderecoId, dto.TransportadoraId,
                                 dto.TipoDestinatario, itens, AliquotaImpostos.Ipi, dto.ValorFrete,
                                 dto.ValorSeguro, dto.ValorOutrasDespesas, dto.ValorDesconto);

            nota.SetNumeroNota(numeroNota);
            nota.SetTipoNota(TipoNotaSaida.Peca);
            nota.SetNaturezaOperacao("Industrialização efetuada para outra empresa");
            nota.SetLocalDestino("sp", Destinatario.Endereco.Estado.Sigla);
            nota.SetOutrasInformacoes(gravaDados? dto.OutrasInformacoes : GenerateOutrasInformacoesPeca(nota));
            
            return nota;
        }

        private NotaSaida EmitirNotaParaEmbalagem(NotaSaidaAddDto dto, bool gravaDados = true)
        {
            var itens = new List<NotaSaidaItens>();

            var numeroNota = gravaDados ? _notaSaidaRepository.GetNotaSaidaSequence().ToString(): "0000";

            foreach (var item in dto.LoteItens)
            {
                var lote = _loteService.GetLoteDadosSaida(item.LoteId);

                if (lote.Id == Guid.Empty)
                {
                    NotifyError("Lote não identificado");
                    return null;
                }

                var loteMovimento = new LoteMovimento(Guid.NewGuid(),
                    DateTime.Now,
                    lote.Id,
                    lote.LoteSequencia,
                    item.Quantidade,
                    item.PrecoUnitario,
                    numeroNota,
                    TipoMovimentoEstoque.Saida,
                    $"Devolução de Vasilhame/Embalagem via Nfe N.{numeroNota}");

                var notaItem = new NotaSaidaItens(
                Guid.NewGuid(),
                lote.CodigoPeca,
                lote.DescricaoPeca,
                lote.CfopSaidaRemessa,
                lote.Ncm,
                lote.UnidadeMedida,
                lote.PrecoSaidaRemessa,
                item.Quantidade,
                0,
                lote.LoteNumero,
                lote.LoteSequencia,
                Guid.Empty
                );
                notaItem.SetSituacaoTributaria(IcmsSituacaoTributariaNfe.NaoTributada,
                    PisSituacaoTributariaNfe.OperacaoIsentaDaContribuicao,
                    CofinsSituacaoTributariaNfe.OperacaoSemIncidenciaDaContribuicao);
                notaItem.SetTipoItem(TipoNotaSaidaItem.Remessa);

                if(gravaDados)
                    _loteService.AddMovimentoSaida(loteMovimento, true);

                itens.Add(notaItem);
            }

            var nota = new NotaSaida(Guid.NewGuid(), dto.DestinatarioId, dto.EnderecoId, dto.TransportadoraId,
                                 dto.TipoDestinatario, itens, AliquotaImpostos.Ipi, dto.ValorFrete,
                                 dto.ValorSeguro, dto.ValorOutrasDespesas, dto.ValorDesconto);

            nota.SetNumeroNota(numeroNota);
            nota.SetTipoNota(TipoNotaSaida.Embalagem);
            nota.SetFinalidadeEmissao(FinalidadeEmissaoNfe.Devolucao);
            nota.SetNaturezaOperacao("Devolucao de vasilhame ou sacaria");
            nota.SetLocalDestino("sp", Destinatario.Endereco.Estado.Sigla);
            nota.SetOutrasInformacoes(gravaDados? dto.OutrasInformacoes : GenerateOutrasInformacoesEmbalagem(nota));
            return nota;
        }

        private NotaSaida EmitirNotaAvulsa(NotaSaidaAddDto dto, bool gravaDados = true)
        {
            var itens = new List<NotaSaidaItens>();

            var numeroNota = gravaDados ? _notaSaidaRepository.GetNotaSaidaSequence().ToString(): "0000";

            foreach (var item in dto.LoteItens)
            {
                var lote = _loteService.GetLoteDadosSaida(item.LoteId);

                if (lote.Id == Guid.Empty)
                {
                    NotifyError("Lote não identificado");
                    return null;
                }

                var loteMovimento = new LoteMovimento(Guid.NewGuid(),
                    DateTime.Now,
                    lote.Id,
                    lote.LoteSequencia,
                    item.Quantidade,
                    item.PrecoUnitario,
                    numeroNota,
                    TipoMovimentoEstoque.Saida,
                    $"Devolução de Peça via Nfe N.{numeroNota}");

                var notaItem = new NotaSaidaItens(
                Guid.NewGuid(),
                lote.CodigoPeca,
                lote.DescricaoPeca,
                item.Cfop,
                lote.Ncm,
                lote.UnidadeMedida,
                lote.PrecoSaidaRemessa,
                item.Quantidade,
                0,
                lote.LoteNumero,
                lote.LoteSequencia,
                Guid.Empty
                );
                notaItem.SetSituacaoTributaria(IcmsSituacaoTributariaNfe.OutrasRegimeNormal,
                    PisSituacaoTributariaNfe.OperacaoIsentaDaContribuicao,
                    CofinsSituacaoTributariaNfe.OperacaoSemIncidenciaDaContribuicao);
                notaItem.SetTipoItem(TipoNotaSaidaItem.Remessa);

                if(gravaDados)
                    _loteService.AddMovimentoSaida(loteMovimento, true);

                itens.Add(notaItem);
            }

            var nota = new NotaSaida(Guid.NewGuid(), dto.DestinatarioId, dto.EnderecoId, dto.TransportadoraId,
                                 dto.TipoDestinatario, itens, AliquotaImpostos.Ipi, dto.ValorFrete,
                                 dto.ValorSeguro, dto.ValorOutrasDespesas, dto.ValorDesconto);

            nota.SetNumeroNota(numeroNota);
            nota.SetTipoNota(TipoNotaSaida.PecaAvulsa);
            nota.SetFinalidadeEmissao(FinalidadeEmissaoNfe.Normal);
            nota.SetNaturezaOperacao("Retorno de mercadoria recebida para industrializacao e nao a");
            nota.SetLocalDestino("sp", Destinatario.Endereco.Estado.Sigla);
            nota.SetOutrasInformacoes(gravaDados? dto.OutrasInformacoes : GenerateOutrasInformacoesPecaAvulsa(nota));
            return nota;
        }

        public AliquotaImpostos GetAliquotaImpostos(string uf)
        {
            var estado = Estado.NovoEstado(uf);

            return new AliquotaImpostos(
                    estado.AliquotaBaseCalculo,
                    0m,
                    estado.AliquotaIcms,
                    0.65m,
                    3m);
        }

        public NotaSaidaDestinatario GetDestinatario()
        {
            return Destinatario;
        }

        private NotaSaidaDestinatario GenerateDestinatario(NotaSaidaAddDto dto)
        {
            Cliente cliente = null;
            Fornecedor fornecedor = null;
            Endereco endereco;

            if (dto.TipoDestinatario == TipoDestinatario.Cliente)
            {
                cliente = _clienteRepository.GetByIdWithEnderecos(dto.DestinatarioId);
                endereco = _clienteRepository.GetEndereco(dto.EnderecoId);
            }

            else
            {
                fornecedor = _fornecedorRepository.GetByIdWithEnderecos(dto.DestinatarioId);
                endereco = _fornecedorRepository.GetEndereco(dto.EnderecoId);
            }

            return new NotaSaidaDestinatario(cliente, fornecedor, endereco);
        }

        private NotaSaidaItens[] GenerateItens(OrdemLote ordemLote)
        {
            var itens = new NotaSaidaItens[2];

            var loteSaida = _loteService.GetLoteDadosSaida(ordemLote.LoteId);

            var itemServico = new NotaSaidaItens(
                Guid.NewGuid(),
                loteSaida.CodigoPecaSaida,
                $"{loteSaida.DescricaoPeca} LT:{ordemLote.GetLoteSequenciaString()} VL:{ordemLote.Validade.Value.ToString("dd/MM/yyyy")}",
                loteSaida.CfopSaidaServico,
                loteSaida.Ncm,
                loteSaida.UnidadeMedida,
                loteSaida.PrecoSaidaServico,
                ordemLote.QtdeProduzida.Value,
                ordemLote.Ordem.OrdemNumero,
                ordemLote.LoteNumero,
                ordemLote.LoteSequencia,
                Guid.Empty
                );
            itemServico.SetSituacaoTributaria(IcmsSituacaoTributariaNfe.TributadaIntegralmente,
                PisSituacaoTributariaNfe.OperacaoTributavelBaseDeCalculoIgualValorOperacaoAliquotaNormalMenoscumulativoDivididoNaocumulativo,
                CofinsSituacaoTributariaNfe.OperacaoTributavelBaseDeCalculoIgualValorDaOperacaoAliquotaNormalMenosCumulativoDivididoNaoCumulativo);
            itemServico.CalcularImpostos(AliquotaImpostos.BaseCalculo, AliquotaImpostos.Icms, AliquotaImpostos.Pis,
                                         AliquotaImpostos.Cofins, AliquotaImpostos.Ipi);
            itemServico.SetTipoItem(TipoNotaSaidaItem.Servico);
            itens[0] = itemServico;

            var itemRemessa = new NotaSaidaItens(
               Guid.NewGuid(),
                loteSaida.CodigoPeca,
                loteSaida.DescricaoPeca,
                loteSaida.CfopSaidaRemessa,
                loteSaida.Ncm,
                loteSaida.UnidadeMedida,
                loteSaida.PrecoSaidaRemessa,
                ordemLote.QtdeProduzida.Value,
                ordemLote.Ordem.OrdemNumero,
                ordemLote.LoteNumero,
                ordemLote.LoteSequencia,
                Guid.Empty
                );
            itemRemessa.SetSituacaoTributaria(IcmsSituacaoTributariaNfe.Suspencao,
                PisSituacaoTributariaNfe.OutrasOperacoesDeSaida,
                CofinsSituacaoTributariaNfe.OutrasOperacoesDeSaida);
            itemRemessa.SetTipoItem(TipoNotaSaidaItem.Remessa);
            itens[1] = itemRemessa;

            return itens;
        }

        private string GenerateOutrasInformacoesPeca(NotaSaida nota)
        {
            var texto = new StringBuilder();

            var textoSP = "Diferimento do ICMS cfart403 e suspensao do ICMS cfart402 item 2 do RICMS 454902000|" +
                $"Suspensao IPI de acordo com art42 inciso VII decreto 45442002.";

            var textoForaSP = "Saida com suspensao do IPI, conforme disposto no inciso II, do paragrafo 7, do art. 29 da lei n. 10.637 de " +
                "30/12/2002, e art. 5 da IN RFB 948 DE 15/06/2009";

            texto.Append(nota.LocalDestino == LocalDestinoNfe.OperacaoInterna ? textoSP : textoForaSP);

            var itensRemessa = nota.Itens.Where(n => n.TipoItem == TipoNotaSaidaItem.Remessa);

            foreach (var i in itensRemessa)
            {
                var lote = _loteRepository.GetByNumero(i.LoteNumero);
                var ehCobertura = !lote.NotaFiscalCobertura.IsNullOrWhiteSpace();
                var notaEntrada = ehCobertura ? lote.NotaFiscalCobertura : lote.NotaFiscal;
                var textoRemessa = $"| Retorno de {i.Quantidade} pecas de codigo {i.Codigo} referente nota fiscal {notaEntrada}, no valor unitario de R$ {i.ValorUnitario}. ";
                texto.Append(textoRemessa);
            }

            return texto.ToString();
        }

        private string GenerateOutrasInformacoesEmbalagem(NotaSaida nota)
        {
            var texto = new StringBuilder();

            var itensRemessa = nota.Itens.Where(n => n.TipoItem == TipoNotaSaidaItem.Remessa);

            foreach (var i in itensRemessa)
            {
                var lote = _loteRepository.GetByNumero(i.LoteNumero);
                var notaEntrada = lote.NotaFiscal;

                var informacoes = $"| Retorno de {i.Quantidade} {i.Descricao} referente nota fiscal {notaEntrada} " +
                $"no valor unitario de R$ {i.ValorUnitario}. ";

                texto.Append(informacoes);
            }

            return texto.ToString();
        }

        private string GenerateOutrasInformacoesPecaAvulsa(NotaSaida nota)
        {
            var texto = new StringBuilder();

            var itensRemessa = nota.Itens.Where(n => n.TipoItem == TipoNotaSaidaItem.Remessa);

            foreach (var i in itensRemessa)
            {
                var lote = _loteRepository.GetByNumero(i.LoteNumero);
                var notaEntrada = lote.NotaFiscal;

                var informacoes = $"| Devolução de {i.Quantidade} {i.Descricao} referente nota fiscal {notaEntrada} " +
                $"no valor unitario de R$ {i.ValorUnitario}. ";

                texto.Append(informacoes);
            }

            return texto.ToString();
        }

        public void Dispose()
        {
            _notaSaidaRepository?.Dispose();
            _loteRepository?.Dispose();
            _ordemRepository?.Dispose();
            _clienteRepository?.Dispose();
            _fornecedorRepository?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

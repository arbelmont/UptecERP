using System;
using System.Collections.Generic;
using System.Linq;
using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Validations;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Producao.Domain.Transportadoras.Models;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Enums.NFe;
using Uptec.Erp.Shared.Domain.Models.Endereco;

namespace Uptec.Erp.Producao.Domain.Fiscal.Models
{
    public class NotaSaida : Entity<NotaSaida>
    {
        public string NumeroNota { get; private set; }
        public string NaturezaOperacao { get; private set; }
        public DateTime Data { get; private set; }
        public decimal AliquotaIpi { get; private set; }
        public decimal ValorBaseCalculo { get; private set; }
        public decimal ValorIcms { get; private set; }
        public decimal ValorTotalProdutos { get; private set; }
        public decimal ValorFrete { get; private set; }
        public decimal ValorSeguro { get; private set; }
        public decimal ValorDesconto { get; private set; }
        public decimal ValorOutrasDespesas { get; private set; }
        public decimal ValorIpi { get; private set; }
        public decimal ValorPis { get; private set; }
        public decimal ValorCofins { get; private set; }
        public decimal ValorTotalNota { get; private set; }
        public TipoDestinatario TipoDestinatario { get; private set; }
        public Guid? ArquivoId { get; private set; }
        public Guid? ClienteId { get; private set; }
        public Guid? FornecedorId { get; private set; }
        public Guid? TransportadoraId { get; private set; }
        public Guid EnderecoId { get; private set; }
        public ModalidadeFreteNfe ModalidadeFrete { get; private set; }
        public LocalDestinoNfe LocalDestino { get; private set; }
        public FinalidadeEmissaoNfe FinalidadeEmissao { get; private set; }
        public StatusNfSaida Status { get; private set; }
        public string OutrasInformacoes { get; private set; }
        public string ErroApi { get; private set; }
        public TipoNotaSaida Tipo { get; private set; }
        public virtual Cliente Cliente { get; private set; }
        public virtual Fornecedor Fornecedor { get; private set; }
        public virtual Transportadora Transportadora { get; private set; }
        public virtual object EnderecoDestinatario { get; private set; }
        public virtual object EnderecoTransportadora { get; private set; }
        public virtual ICollection<NotaSaidaItens> Itens { get; private set; }

        protected NotaSaida()
        {
            Itens = new List<NotaSaidaItens>();
            Validation = new Validation(new FluentValidation.Results.ValidationResult(), new FluentValidation.Results.ValidationResult());
        }

        public NotaSaida(Guid id, Guid destinatarioId, Guid enderecoId, Guid? transportadoraId, TipoDestinatario tipoDestinatario, 
                         List<NotaSaidaItens> itens, decimal aliquotaIpi, decimal valorFrete, decimal valorSeguro, 
                         decimal valorOutrasDespesas, decimal valorDesconto)
        {
            Id = id;
            TransportadoraId = transportadoraId == Guid.Empty? null : transportadoraId;
            Data = DateTime.Now;
            AliquotaIpi = aliquotaIpi;
            Itens = itens;
            ValorFrete = valorFrete;
            ValorSeguro = valorSeguro;
            ValorOutrasDespesas = valorOutrasDespesas;
            ValorDesconto = valorDesconto;
            ValorTotalProdutos = Itens.Sum(i => i.ValorTotal);
            CalcularImpostos(aliquotaIpi);
            CalcularTotal();
            SetDestinatario(destinatarioId, enderecoId, tipoDestinatario);
            SetStatus(StatusNfSaida.Processando);
            ModalidadeFrete = ModalidadeFreteNfe.PorContaDoDestinatario;
            FinalidadeEmissao = FinalidadeEmissaoNfe.Normal;
        }

        public override bool IsValid()
        {
            Validation = new Validation(new NotaSaidaValidation().Validate(this), new NotaSaidaSystemValidation().Validate(this));
            return Validation.IsValid();
        }

        public void SetNumeroNota(string numero)
        {
            NumeroNota = numero;
        }

        public void SetNaturezaOperacao(string natureza)
        {
            NaturezaOperacao = natureza;
        }

        public void SetArquivoId(Guid arquivoId)
        {
            ArquivoId = arquivoId;
        }

        public void SetStatus(StatusNfSaida status, string erroApi = "")
        {
            Status = status;
            ErroApi = erroApi;
        }

        public void SetModalidadeFrete(ModalidadeFreteNfe modalidade)
        {
            ModalidadeFrete = modalidade;
        }

        public void SetLocalDestino(string ufEmitente, string ufDestinatario)
        {
            LocalDestino = (ufEmitente.ToLower() == ufDestinatario.ToLower()? LocalDestinoNfe.OperacaoInterna : LocalDestinoNfe.OperacaoInterestadual);
        }

        public void SetFinalidadeEmissao(FinalidadeEmissaoNfe finalidade)
        {
            FinalidadeEmissao = finalidade;
        }

        public void SetCliente(Cliente cliente, ClienteEndereco endereco)
        {
            Cliente = cliente;
            EnderecoDestinatario = endereco;
        }

        public void SetFornecedor(Fornecedor fornecedor, FornecedorEndereco endereco)
        {
            Fornecedor = fornecedor;
            EnderecoDestinatario = endereco;
        }

        public void SetEnderecoDestinatario(Endereco endereco)
        {
            EnderecoDestinatario = endereco;
        }

        public void SetEnderecoTransportadora(Endereco endereco)
        {
            EnderecoTransportadora = endereco;
        }

        public void SetOutrasInformacoes(string info)
        {
            OutrasInformacoes = info;
        }

        public void SetTipoNota(TipoNotaSaida tipo)
        {
            Tipo = tipo;
        }

        private void CalcularImpostos(decimal aliquotaIpi)
        {
            ValorBaseCalculo = Itens.Sum(i => i.ValorBaseCalculo);
            ValorIcms = Itens.Sum(i => i.ValorIcms);
            ValorIpi = Itens.Sum(i => i.ValorIpi);
            ValorPis = Itens.Sum(i => i.ValorPis);
            ValorCofins = Itens.Sum(i => i.ValorCofins);
        }


        private void CalcularTotal()
        {
            ValorTotalNota = (ValorTotalProdutos + ValorFrete + ValorSeguro + ValorOutrasDespesas) - ValorDesconto;
        }

        private void SetDestinatario(Guid destinatarioId, Guid enderecoId, TipoDestinatario tipo)
        {
            EnderecoId = enderecoId;
            TipoDestinatario = tipo;
            if (TipoDestinatario == TipoDestinatario.Cliente)
                ClienteId = destinatarioId;
            else
                FornecedorId = destinatarioId;
        }

        public override void Delete()
        {
            Deleted = true;
            NumeroNota = NumeroNota + $"*{DateTime.Now.ToString()}";
        }

        public const byte NumeroNotaMaxLenght = 50;
        public const byte CfopMaxLenght = 5;
        public const byte NomeEmissorMaxLenght = 100;
        public const int ErroApiMaxLenght = 1000;
        public const int OutrasInformacoesMaxLenght = 1000;
        public const byte NaturezaOperacaoMaxLenght = 200;


        public static class NotaSaidaFactory
        {
            public static NotaSaida NotaSaidaCompleta(
                    Guid id,
                    string numeroNota,
                    DateTime data,
                    decimal valorBaseCalculo,
                    decimal valorIcms,
                    decimal valorTotalProdutos,
                    decimal valorFrete,
                    decimal valorSeguro,
                    decimal valorDesconto,
                    decimal valorOutrasDespesas,
                    decimal valorIpi,
                    decimal valorTotalNota,
                    TipoDestinatario tipoDestinatario,
                    Guid clienteId,
                    Guid fornecedorId,
                    List<NotaSaidaItens> itens
                )
            {
                var notaSaida = new NotaSaida {
                    Id = id,
                    NumeroNota = numeroNota,
                    Data = data,
                    ValorBaseCalculo = valorBaseCalculo,
                    ValorIcms = valorIcms,
                    ValorTotalProdutos = valorTotalProdutos,
                    ValorFrete = valorFrete,
                    ValorSeguro = valorSeguro,
                    ValorDesconto = valorDesconto,
                    ValorOutrasDespesas = valorOutrasDespesas,
                    ValorIpi = valorIpi,
                    ValorTotalNota = valorTotalNota,
                    TipoDestinatario = tipoDestinatario,
                    ClienteId = clienteId,
                    FornecedorId = fornecedorId,
                    Itens = itens
                };

                return notaSaida;
            }
        }
    }
}
       

using System;
using System.Collections.Generic;
using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Producao.Domain.Lotes.Validations;
using Uptec.Erp.Producao.Domain.Ordens.Models;
using Uptec.Erp.Producao.Domain.Pecas.Models;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Lotes.Models
{
    public class Lote : Entity<Lote>
    {
        public DateTime Data { get; private set; }
        public int LoteNumero { get; private set; }
        public decimal Quantidade { get; private set; }
        public decimal Saldo { get; private set; }
        public decimal PrecoEntrada { get; private set; }
        public string CfopEntrada { get; private set; }
        public string NotaFiscal { get; private set; }
        public string NotaFiscalCobertura { get; private set; }
        public LoteStatus Status { get; private set; }
        public DateTime DataFabricacao { get; private set; }
        public DateTime DataValidade { get; private set; }
        public string Localizacao { get; private set; }
        public decimal QtdeConcilia { get; private set; }
        public Guid PecaId { get; private set; }
        public TipoPeca TipoPeca { get; private set; }
        public Guid? ClienteId { get; private set; }
        public Guid? FornecedorId { get; private set; }
        public int Sequencia { get; private set; }
        public bool EhCobertura { get; private set; }
        public virtual Peca Peca { get; private set; }
        public virtual Cliente Cliente { get; private set; }
        public virtual Fornecedor Fornecedor { get; private set; }
        public virtual ICollection<LoteMovimento> Movimentos { get; private set; }
        public virtual ICollection<OrdemLote> OrdemLotes { get; private set; }

        protected Lote()
        {
        }

        public Lote(Guid id, DateTime data, Guid pecaId, TipoPeca tipoPeca,
                    decimal quantidade, decimal precoEntrada, string cfopEntrada, string notaFiscal,
                    DateTime dataFabricacao, DateTime dataValidade, string localizacao, decimal qtdeConcilia)
        {
            Id = id;
            Data = data;
            PecaId = pecaId;
            TipoPeca = tipoPeca;
            Quantidade = quantidade;
            Saldo = quantidade;
            CfopEntrada = cfopEntrada;
            PrecoEntrada = precoEntrada;
            NotaFiscal = notaFiscal;
            DataFabricacao = dataFabricacao;
            DataValidade = dataValidade;
            Localizacao = localizacao;
            QtdeConcilia = qtdeConcilia;
            Sequencia = 0;
            EhCobertura = false;
            UpdateStatus();
        }

        public override bool IsValid()
        {
            Validation = new Validation(new LoteValidation().Validate(this), new LoteSystemValidation().Validate(this));

            return Validation.IsValid();
        }
        public void AddQuantidade(decimal quantidade)
        {
            Saldo += quantidade;
            UpdateStatus();
        }

        public void AddSequencia()
        {
            Sequencia++;
        }

        public void SubQuantidade(decimal quantidade)
        {
            Saldo -= quantidade;
            UpdateStatus();
        }

        public void SetSaldo(decimal saldo)
        {
            Saldo = saldo;
            UpdateStatus();
        }

        public void SetNotaFiscalCobertura(string numeroNota)
        {
            NotaFiscalCobertura = numeroNota;
        }

        public void SetLoteNumero(int numero)
        {
            LoteNumero = numero;
        }

        public void SetCliente(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public void SetPrecoEntrada(decimal valor)
        {
            PrecoEntrada = valor;
        }

        public void SetCfopEntrada(string cfop)
        {
            CfopEntrada = cfop;
        }

        public void SetFornecedor(Guid fornecedorId)
        {
            FornecedorId = fornecedorId;
        }

        public void SetEhCobertura()
        {
            EhCobertura = true;
        }

        private void UpdateStatus()
        {
            Status = (Saldo == 0 ? LoteStatus.Fechado : LoteStatus.Aberto);
        }

        public const int LoteNumeroMaxLenght = int.MaxValue;
        public const byte NotaFiscalMaxLenght = 50; //TODO: Remover ap�s implementa��o de NotaFiscal
        public const byte CfopEntradaMaxLenght = 5;
        public const byte LocalizacaoMaxLenght = 30;
    }
}
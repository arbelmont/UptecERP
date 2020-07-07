using System;
using System.Collections.Generic;
using System.Linq;
using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Producao.Domain.Ordens.Validations;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Ordens.Models
{
    public class Ordem : Entity<Ordem>
    {
        public int OrdemNumero { get; private set; }
        public DateTime DataEmissao { get; private set; }
        public DateTime? DataProducao { get; private set; }
        public decimal QtdeTotal { get; private set; }
        public decimal? QtdeTotalProduzida { get; private set; }
        public OrdemMotivoExpedicao MotivoExpedicao { get; private set; }
        public StatusOrdem Status { get; private set; }
        public Guid? ClienteId { get; private set; }
        public Guid? FornecedorId { get; private set; }
        public string CodigoPeca { get; private set; }
        public string DescricaoPeca { get; private set; }
        public ICollection<OrdemLote> OrdemLotes { get; private set; }
        public virtual Cliente Cliente { get; private set; }
        public virtual Fornecedor Fornecedor { get; private set; }

        public Ordem(Guid id, decimal qtdeTotal, List<OrdemLote> ordemLotes)
        {
            Id = id;
            DataEmissao = DateTime.Now;
            QtdeTotal = qtdeTotal;
            OrdemLotes = ordemLotes;
            Status = StatusOrdem.Producao;
            MotivoExpedicao = OrdemMotivoExpedicao.Produzindo;
            Validation = new Validation(new FluentValidation.Results.ValidationResult(), new FluentValidation.Results.ValidationResult());
        }


        protected Ordem()
        {
            OrdemLotes = new List<OrdemLote>();
            Validation = new Validation(new FluentValidation.Results.ValidationResult(), new FluentValidation.Results.ValidationResult());
        }

        public void SetCliente(Guid clienteId)
        {
            ClienteId = clienteId;
        }

        public void SetFornecedor(Guid fornecedorId)
        {
            FornecedorId = fornecedorId;
        }

        public void SetProducao(decimal qtdeTotalProduzida)
        {
            DataProducao = DateTime.Now;
            QtdeTotalProduzida = qtdeTotalProduzida;
        }

        public void SetOrdemNumero(int numero)
        {
            OrdemNumero = numero;
        }

        public void SetStatus(StatusOrdem status)
        {
            Status = status;
        }

        public void UpdateStatus()
        {
            if (OrdemLotes.Any(ol => ol.NotaFiscalSaida == null))
                Status = StatusOrdem.Expedicao;
            else
                Status = StatusOrdem.Finalizada;
        }

        //public void SetOrdemLotes(List<OrdemLote> ordemLotes)
        //{
        //    OrdemLotes = ordemLotes;
        //}

        public override bool IsValid()
        {
            Validation = new Validation(new OrdemValidation().Validate(this), new OrdemSystemValidation().Validate(this));

            return Validation.IsValid();
        }

        public void SetPeca(string codigo, string descricao)
        {
            CodigoPeca = codigo;
            DescricaoPeca = descricao;
        }

        public const byte CodigoMaxLenght = 30;
        public const byte DescricaoMaxLenght = 50;
    }
}
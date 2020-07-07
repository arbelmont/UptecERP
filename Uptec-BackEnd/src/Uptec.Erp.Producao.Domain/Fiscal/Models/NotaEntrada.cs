using System;
using System.Collections.Generic;
using System.Linq;
using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using Uptec.Erp.Producao.Domain.Fiscal.Validations;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Cfop;
using Uptec.Erp.Shared.Domain.ValueObjects;
using Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf;

namespace Uptec.Erp.Producao.Domain.Fiscal.Models
{
    public class NotaEntrada : Entity<NotaEntrada>
    {
        public string NumeroNota { get; private set; }
        public DateTime Data { get; private set; }
        public DateTime? DataConciliacao { get; private set; }
        public decimal Valor { get; private set; }
        public string Cfop { get; private set; }
        public Cnpj CnpjEmissor { get; private set; }
        public string NomeEmissor { get; private set; }
        public Email EmailEmissor { get; private set; }
        public TipoEmissor TipoEmissor { get; private set; }
        public TipoEstoque TipoEstoque {get; private set; }
        public StatusNfEntrada Status { get; private set; }
        public Guid ArquivoId { get; private set; }
        public virtual ICollection<NotaEntradaItens> Itens { get; private set; }
        public ICollection<string> Inconsistencias { get; private set; }

        protected NotaEntrada()
        {
            Inconsistencias = new List<string>();
            Itens = new List<NotaEntradaItens>();
            Validation = new Validation(new FluentValidation.Results.ValidationResult(), new FluentValidation.Results.ValidationResult());
        }

        public NotaEntrada(Guid id, string numeroNota, DateTime data, decimal valor, string cfop, 
                           string cnpjEmissor, string nomeEmissor, string emailEmissor, List<NotaEntradaItens> itens)
        {
            Id = id;
            NumeroNota = numeroNota;
            Data = data;
            Valor = valor;
            Cfop = cfop;
            CnpjEmissor = new Cnpj(cnpjEmissor);
            NomeEmissor = nomeEmissor;
            EmailEmissor = new Email(emailEmissor);
            SetTipoEmissor(cfop);
            SetTipoEstoque(cfop);
            Status = StatusNfEntrada.Conciliar;
            Inconsistencias = new List<string>();
            Itens = itens;
        }

        public override bool IsValid()
        {
            Validation = new Validation(new NotaEntradaValidation().Validate(this), new NotaEntradaSystemValidation().Validate(this));
            return Validation.IsValid();
        }

        public void SetStatus()
        {
            if(Itens.Any(i => i.Status == StatusNfEntradaItem.Conciliar))
            {
                Status = StatusNfEntrada.Conciliar;
                return;
            }

            if (Itens.Any(i => i.Status == StatusNfEntradaItem.ACobrir))
            {
                Status = StatusNfEntrada.ACobrir;
                return;
            }

            Status = StatusNfEntrada.Recebida;
        }

        public void SetArquivoId(Guid arquivoId)
        {
            ArquivoId = arquivoId;
        }

        public void SetDataConciliacao(DateTime data)
        {
            DataConciliacao = data;
        }

        public override void Delete()
        {
            Deleted = true;
            NumeroNota = NumeroNota + $"*{DateTime.Now.ToString()}";
        }

        private void SetTipoEmissor(string cfop)
        {
            //com base no cfop, determinar se emissor é cliente ou fornecedor
            if(CfopUptec.CfopsEntradaClientePeca.Contains(cfop))
            {
                TipoEmissor = TipoEmissor.Cliente;
                return;
            }

            if (CfopUptec.CfopsEntradaFornecedorPeca.Contains(cfop) ||
                CfopUptec.CfopsEntradaComponente.Contains(cfop))
            {
                TipoEmissor = TipoEmissor.Fornecedor;
                return;
            }

            TipoEmissor = TipoEmissor.Indefinido;
        }

        public void SetTipoEmissor(TipoEmissor tipo)
        {
            TipoEmissor = tipo;
        }

        private void SetTipoEstoque(string cfop)
        {
            //com base no cfop determinar o tipo de estoque
            if (CfopUptec.CfopsEntradaPeca.Contains(cfop))
                TipoEstoque = TipoEstoque.Peca;
            else if(CfopUptec.CfopsEntradaComponente.Contains(cfop))
                TipoEstoque = TipoEstoque.MateriaPrima;
        }

        public const byte NumeroNotaMaxLenght = 50; //TODO Ajustar após verificar tamanho padrão com o Drezé!
        public const byte CfopMaxLenght = 5;
        public const byte NomeEmissorMaxLenght = 100;
    }
}
       

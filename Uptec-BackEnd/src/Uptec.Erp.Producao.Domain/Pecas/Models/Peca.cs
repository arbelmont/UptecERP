using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using System;
using System.Collections.Generic;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Producao.Domain.Pecas.Validations;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Pecas.Models
{
    public class Peca : Entity<Peca>
    {
        public string Codigo { get; private set; }
        public string CodigoSaida { get; private set; }
        public string Descricao { get; private set; }
        public UnidadeMedida Unidade { get; private set; }
        public TipoPeca Tipo { get; private set; }
        public decimal Preco { get; private set; }
        public decimal PrecoSaida { get; private set; }
        public string Ncm { get; private set; }
        public string Revisao { get; private set; }
        public Guid ClienteId { get; private set; }
        public virtual Cliente Cliente { get; private set; }
        public virtual ICollection<PecaComponente> Componentes { get; private set; }
        public virtual ICollection<PecaFornecedorCodigo> CodigosFornecedor { get; private set; }
        public virtual ICollection<Lote> Lotes { get; private set; }
        protected Peca()
        {
        }

        public Peca(Guid id, string codigo, string codigoSaida, string descricao, UnidadeMedida unidade,
                    TipoPeca tipo, decimal preco, decimal precoSaida, string ncm, string revisao, Guid clienteId,
                    ICollection<PecaComponente> componentes, ICollection<PecaFornecedorCodigo> codigosFornecedor)
        {
            Id = (id == Guid.Empty ? Guid.NewGuid() : id);
            Codigo = codigo;
            CodigoSaida = codigoSaida;
            Descricao = descricao;
            Unidade = unidade;
            Tipo = tipo;
            Preco = preco;
            PrecoSaida = precoSaida;
            Ncm = ncm;
            Revisao = revisao;
            ClienteId = clienteId;
            Componentes = componentes;
            CodigosFornecedor = codigosFornecedor;
        }

        public override bool IsValid()
        {
            Validation = new Validation(new PecaValidation().Validate(this), new PecaSystemValidation().Validate(this));
            return Validation.IsValid();
        }

        public const byte CodigoMaxLenght = 30;
        public const byte CodigoSaidaMaxLenght = 30;
        public const byte DescricaoMaxLenght = 50;
        public const byte NcmMaxLenght = 12;
        public const byte RevisaoMaxLenght = 40;
    }
}
using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using System;
using System.Collections.Generic;
using Uptec.Erp.Producao.Domain.Componentes.Validations;
using Uptec.Erp.Producao.Domain.Pecas.Models;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Componentes.Models
{
    public class Componente : Entity<Componente>
    {
        public string Codigo { get; private set; }
        public string Descricao { get; private set; }
        public UnidadeMedida Unidade { get; private set; }
        public decimal Preco { get; private set; }
        public string Ncm { get; private set; }
        public decimal Quantidade { get; private set; }
        public decimal QuantidadeMinima { get; private set; }
        public virtual ICollection<PecaComponente> Pecas { get; private set; }
        public virtual ICollection<ComponenteMovimento> Movimentos { get; private set; }

        public override bool IsValid()
        {
            Validation = new Validation(new ComponenteValidation().Validate(this), new ComponenteSystemValidation().Validate(this));

            return Validation.IsValid();
        }

        protected Componente()
        {
            Pecas = new List<PecaComponente>();
        }

        public Componente(Guid id, string codigo, string descricao, UnidadeMedida unidade, decimal preco, string ncm, decimal quantidade, decimal quantidadeMinima)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
            Unidade = unidade;
            Preco = preco;
            Ncm = ncm;
            Quantidade = quantidade;
            QuantidadeMinima = quantidadeMinima;

            Pecas = new List<PecaComponente>();
        }

        public void AddQuantidade(decimal quantidade)
        {
            Quantidade += quantidade;
        }

        public void SubQuantidade(decimal quantidade)
        {
            Quantidade -= quantidade;
        }

        public const byte CodigoMaxLenght = 30;
        public const byte DescricaoMaxLenght = 50;
        public const byte NcmMaxLenght = 12;
    }
}
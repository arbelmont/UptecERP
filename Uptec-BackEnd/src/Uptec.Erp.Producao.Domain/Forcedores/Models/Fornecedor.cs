using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using System;
using System.Collections.Generic;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fornecedores.Validations;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Producao.Domain.Ordens.Models;
using Uptec.Erp.Producao.Domain.Pecas.Models;
using Uptec.Erp.Shared.Domain.ValueObjects;
using Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf;

namespace Uptec.Erp.Producao.Domain.Fornecedores.Models
{
    public class Fornecedor : Entity<Fornecedor>
    {
        public Cnpj Cnpj { get; private set; }
        public string InscricaoEstadual { get; private set; }
        public string RazaoSocial { get; private set; }
        public string NomeFantasia { get; private set; }
        public Email Email { get; private set; }
        public string Website { get; private set; }
        public string Observacoes { get; private set; }
        public virtual FornecedorEndereco Endereco { get; private set; }
        public virtual FornecedorTelefone Telefone { get; private set; }
        public virtual ICollection<FornecedorEndereco> Enderecos { get; private set; }
        public virtual ICollection<FornecedorTelefone> Telefones { get; private set; }
        public virtual ICollection<Lote> Lotes { get; private set; }
        public virtual ICollection<Ordem> Ordens { get; private set; }
        public virtual ICollection<PecaFornecedorCodigo> PecaCodigosFornecedor { get; private set; }
        public virtual ICollection<NotaSaida> NotasSaida { get; private set; }

        protected Fornecedor()
        {
            Enderecos = new List<FornecedorEndereco>();
            Telefones = new List<FornecedorTelefone>();
            PecaCodigosFornecedor = new List<PecaFornecedorCodigo>();
        }

        public Fornecedor(Guid id,
            string cnpj,
            string inscricaoEstadual,
            string razaoSocial,
            string nomeFantasia,
            string email,
            string website,
            string observacoes,
            FornecedorEndereco endereco,
            FornecedorTelefone telefone
            )
        {
            Id = id;
            Cnpj = new Cnpj(cnpj);
            InscricaoEstadual = inscricaoEstadual;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            Email = new Email(email);
            Website = website;
            Observacoes = observacoes;
            Endereco = endereco;
            Telefone = telefone;
            Enderecos = new List<FornecedorEndereco>(); 
            Telefones = new List<FornecedorTelefone>();
            PecaCodigosFornecedor = new List<PecaFornecedorCodigo>();
        }

        public override bool IsValid()
        {
            Validation = new Validation(new FornecedorValidation().Validate(this), new FornecedorSystemValidation().Validate(this));

            return Validation.IsValid();
        }

        public const byte NomeFantasiaMaxLength = 200;
        public const byte RazaoSocialMaxLength = 200;
        public const byte InscricaoEstadualMaxLength = 16;
        public const byte WebsiteMaxLength = 200;
        public const int ObservacoesMaxLength = 1000;
    }
}
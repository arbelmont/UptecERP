using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using System;
using System.Collections.Generic;
using Uptec.Erp.Producao.Domain.Clientes.Validations;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Lotes.Models;
using Uptec.Erp.Producao.Domain.Ordens.Models;
using Uptec.Erp.Producao.Domain.Transportadoras.Models;
using Uptec.Erp.Shared.Domain.ValueObjects;
using Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf;

namespace Uptec.Erp.Producao.Domain.Clientes.Models
{
    public class Cliente : Entity<Cliente>
    {
        public Cnpj Cnpj { get; private set; }
        public string InscricaoEstadual { get; private set; }
        public string RazaoSocial { get; private set; }
        public string NomeFantasia { get; private set; }
        public Email Email { get; private set; }
        public string Website { get; private set; }
        public string Observacoes { get; private set; }
        public Guid? TransportadoraId { get; private set; }
        public virtual ClienteEndereco Endereco { get; private set; }
        public virtual ClienteTelefone Telefone { get; private set; }
        public virtual ICollection<ClienteEndereco> Enderecos { get; private set; }
        public virtual ICollection<ClienteTelefone> Telefones { get; private set; }
        public virtual ICollection<Lote> Lotes { get; private set; }
        public virtual ICollection<Ordem> Ordens { get; private set; }
        public virtual ICollection<NotaSaida> NotasSaida { get; private set; }
        public virtual Transportadora Transportadora { get; private set; }

        protected Cliente()
        {
            Enderecos = new List<ClienteEndereco>();
            Telefones = new List<ClienteTelefone>();
        }

        public Cliente(Guid id,
            string cnpj,
            string inscricaoEstadual,
            string razaoSocial,
            string nomeFantasia,
            string email,
            string website,
            string observacoes,
            ClienteEndereco endereco,
            ClienteTelefone telefone,
            Guid? transportadoraId = null
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
            Enderecos = new List<ClienteEndereco>(); 
            Telefones = new List<ClienteTelefone>();
            TransportadoraId = transportadoraId;
        }

        public override bool IsValid()
        {
            Validation = new Validation(new ClienteValidation().Validate(this), new ClienteSystemValidation().Validate(this));

            return Validation.IsValid();
        }

        public const byte NomeFantasiaMaxLength = 200;
        public const byte RazaoSocialMaxLength = 200;
        public const byte InscricaoEstadualMaxLength = 16;
        public const byte WebsiteMaxLength = 200;
        public const int ObservacoesMaxLength = 1000;
    }
}
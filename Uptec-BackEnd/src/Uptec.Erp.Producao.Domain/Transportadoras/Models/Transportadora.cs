using System;
using System.Collections.Generic;
using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Transportadoras.Validations;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.ValueObjects;
using Uptec.Erp.Shared.Domain.ValueObjects.CnpjOuCpf;

namespace Uptec.Erp.Producao.Domain.Transportadoras.Models
{
    public class Transportadora : Entity<Transportadora>
    {
        public Cnpj Cnpj { get; private set; }
        public string InscricaoEstadual { get; private set; }
        public string RazaoSocial { get; private set; }
        public string NomeFantasia { get; private set; }
        public Email Email { get; private set; }
        public string Website { get; private set; }
        public TransportadoraTipoEntregaPadrao TipoEntregaPadrao { get; private set; }
        public string Observacoes { get; private set; }
        public virtual TransportadoraEndereco Endereco { get; private set; }
        public virtual TransportadoraTelefone Telefone { get; private set; }
        public virtual ICollection<Cliente> Clientes { get; private set; }
        public virtual ICollection<TransportadoraEndereco> Enderecos { get; private set; }
        public virtual ICollection<TransportadoraTelefone> Telefones { get; private set; }
        public virtual ICollection<NotaSaida> NotasSaida { get; private set; }

        protected Transportadora(){
            Enderecos = new List<TransportadoraEndereco>();
            Telefones = new List<TransportadoraTelefone>();
        }

        public Transportadora(Guid id, 
                              string cnpj, 
                              string inscricaoEstadual, 
                              string razaoSocial, 
                              string nomeFantasia, 
                              string email, 
                              string website,
                              TransportadoraTipoEntregaPadrao tipoEntregaPadrao, 
                              string observacoes,
                              TransportadoraEndereco endereco,
                              TransportadoraTelefone telefone)
        {
            Id = id;
            Cnpj = new Cnpj(cnpj);
            InscricaoEstadual = inscricaoEstadual;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            Email = new Email(email);
            Website = website;
            TipoEntregaPadrao = tipoEntregaPadrao;
            Observacoes = observacoes;
            Endereco = endereco;
            Telefone = telefone;
            Enderecos = new List<TransportadoraEndereco>();
            Telefones = new List<TransportadoraTelefone>();
        }

        public override bool IsValid()
        {
            Validation = new Validation(new TransportadoraValidation().Validate(this), new TransportadoraSystemValidation().Validate(this));

            return Validation.IsValid();
        }

        public const byte NomeFantasiaMaxLength = 200;
        public const byte RazaoSocialMaxLength = 200;
        public const byte InscricaoEstadualMaxLength = 16;
        public const byte WebsiteMaxLength = 200;
        public const int ObservacoesMaxLength = 1000;
    }
}
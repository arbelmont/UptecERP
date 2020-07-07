using System;
using Definitiva.Shared.Domain.Models;
using Definitiva.Shared.Domain.Validations;
using Definitiva.Shared.Infra.Support.Helpers;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Telefone.Validations;

namespace Uptec.Erp.Shared.Domain.Models.Telefone
{
    public abstract class Telefone : Entity<Telefone>
    {
        public string Numero { get; private set; }
        public TelefoneTipo Tipo { get; private set; }
        public bool Whatsapp { get; private set; }
        public string Observacoes { get; private set; }
        public bool Obrigatorio { get; private set; }
        public string Contato { get; private set; }

        protected Telefone() { }

        protected Telefone(Guid id, string numero, TelefoneTipo tipo,
                           bool whatsapp, string observacoes, bool obrigatorio = true,
                           string contato = "")
        {
            Id = id;
            Numero = numero;
            Tipo = tipo;
            Whatsapp = whatsapp;
            Observacoes = observacoes.ReplaceNull();
            Obrigatorio = obrigatorio;
            Contato = contato;
        }

        public override bool IsValid()
        {
            Validation = new Validation(new TelefoneValidation(Obrigatorio).Validate(this));

            return Validation.IsValid();
        }



        public const byte NumeroMaxLength = 11;
        public const int ObservacoesMaxLength = 1000;
        public const byte ContatoMaxLength = 100;
    }
}
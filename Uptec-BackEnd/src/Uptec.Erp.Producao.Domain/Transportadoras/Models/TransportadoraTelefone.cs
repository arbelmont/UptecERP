using System;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Telefone;

namespace Uptec.Erp.Producao.Domain.Transportadoras.Models
{
    public class TransportadoraTelefone : Telefone
    {
        public Guid TransportadoraId { get; private set; }
        public virtual Transportadora Transportadora { get; private set; }

        protected TransportadoraTelefone() { }

        public TransportadoraTelefone(Guid id, Guid transportadoraId, string numero, TelefoneTipo tipo, bool whatsapp, string observacoes, string contato) 
            : base(id, numero, tipo, whatsapp, observacoes, TelefoneObrigatorio, contato)
        {
            TransportadoraId = transportadoraId;
        }

        public const bool TelefoneObrigatorio = false;
    }
}

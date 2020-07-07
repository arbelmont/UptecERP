using System;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Telefone;

namespace Uptec.Erp.Producao.Domain.Clientes.Models
{
    public class ClienteTelefone : Telefone
    {
        public Guid ClienteId { get; private set; }
        public virtual Cliente Cliente { get; private set; }

        protected ClienteTelefone() { }

        public ClienteTelefone(Guid id, Guid clienteId, string numero, TelefoneTipo tipo,
                               bool whatsapp, string observacoes, string contato)
            : base(id, numero, tipo, whatsapp, observacoes, TelefoneObrigatorio, contato)
        {
            ClienteId = clienteId;
        }

        public const bool TelefoneObrigatorio = false;
    }
}

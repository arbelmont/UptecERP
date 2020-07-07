using System;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Telefone;

namespace Uptec.Erp.Producao.Domain.Fornecedores.Models
{
    public class FornecedorTelefone : Telefone
    {
        public Guid FornecedorId { get; private set; }
        public virtual Fornecedor Fornecedor { get; private set; }

        protected FornecedorTelefone() { }

        public FornecedorTelefone(Guid id, Guid fornecedorId, string numero, TelefoneTipo tipo,
                               bool whatsapp, string observacoes, string contato)
            : base(id, numero, tipo, whatsapp, observacoes, TelefoneObrigatorio, contato)
        {
            FornecedorId = fornecedorId;
        }

        public const bool TelefoneObrigatorio = false;
    }
}

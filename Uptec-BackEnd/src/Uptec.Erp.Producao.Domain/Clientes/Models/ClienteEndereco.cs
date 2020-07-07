using System;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Endereco;

namespace Uptec.Erp.Producao.Domain.Clientes.Models
{
    public class ClienteEndereco : Endereco
    {
        public Guid ClienteId { get; private set; }
        public virtual Cliente Cliente { get; private set; }

        protected ClienteEndereco() { }

        public ClienteEndereco(Guid id, Guid clienteId, string logradouro,
                               string numero, string complemento, string bairro,
                               string cep, string cidade, string estado, EnderecoTipo tipo) :
            base(id, logradouro, numero, complemento, bairro, cep,
                 cidade, estado, tipo, EnderecoObrigatorio)
        {
            ClienteId = clienteId;
        }

        public const bool EnderecoObrigatorio = false;
    }
}

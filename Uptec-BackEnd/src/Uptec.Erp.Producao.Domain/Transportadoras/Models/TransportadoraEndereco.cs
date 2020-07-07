using System;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Endereco;

namespace Uptec.Erp.Producao.Domain.Transportadoras.Models
{
    public class TransportadoraEndereco : Endereco
    {
        public Guid TransportadoraId { get; private set; }
        public virtual Transportadora Transportadora { get; private set; }
        public const bool EnderecoObrigatorio = false;

        protected TransportadoraEndereco() { }

        public TransportadoraEndereco(Guid id, Guid transportadoraId, string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado, EnderecoTipo tipo) : 
            base(id, logradouro, numero, complemento, bairro, cep, cidade, estado, tipo, EnderecoObrigatorio)
        {
            TransportadoraId = transportadoraId;
        }


    }
}
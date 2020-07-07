using System;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Endereco;

namespace Uptec.Erp.Producao.Domain.Fornecedores.Models
{
    public class FornecedorEndereco : Endereco
    {
        public Guid FornecedorId { get; private set; }
        public virtual Fornecedor Fornecedor { get; private set; }

        protected FornecedorEndereco() { }

        public FornecedorEndereco(Guid id, Guid fornecedorId, string logradouro,
                               string numero, string complemento, string bairro,
                               string cep, string cidade, string estado, EnderecoTipo tipo) :
            base(id, logradouro, numero, complemento, bairro, cep,
                 cidade, estado, tipo, EnderecoObrigatorio)
        {
            FornecedorId = fornecedorId;
        }

        public const bool EnderecoObrigatorio = false;
    }
}

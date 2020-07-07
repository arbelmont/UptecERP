using System;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;

namespace Uptec.Erp.Producao.Domain.Pecas.Models
{
    public class PecaFornecedorCodigo
    {
        public Guid PecaId { get; private set; }
        public Guid FornecedorId { get; private set; }
        public string FornecedorCodigo { get; private set; }

        public virtual Peca Peca { get; private set; }
        public virtual Fornecedor Fornecedor { get; private set; }

        protected PecaFornecedorCodigo() { }
        public PecaFornecedorCodigo(Guid pecaId, Guid fornecedorId, string fornecedorCodigo)
        {
            PecaId = pecaId;
            FornecedorId = fornecedorId;
            FornecedorCodigo = fornecedorCodigo;
        }

        public const byte FornecedorCodigoMaxLenght = 30;
    }
}

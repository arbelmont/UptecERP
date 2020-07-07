using System;
using System.Collections.Generic;
using System.Text;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Shared.Domain.Models.Endereco;

namespace Uptec.Erp.Producao.Domain.Fiscal.Models.Dtos
{
    public class NotaSaidaDestinatario
    {
        public Cliente Cliente { get; private set; }
        public Fornecedor Fornecedor { get; private set; }
        public Endereco Endereco { get; private set; }

        public NotaSaidaDestinatario(Cliente cliente, Fornecedor fornecedor, Endereco endereco)
        {
            Cliente = cliente;
            Fornecedor = fornecedor;
            Endereco = endereco;
        }
    }
}

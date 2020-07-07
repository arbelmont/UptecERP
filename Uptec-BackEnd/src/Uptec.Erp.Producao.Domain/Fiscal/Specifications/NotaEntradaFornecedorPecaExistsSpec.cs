using Definitiva.Shared.Domain.DomainValidator;
using System;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Producao.Domain.Pecas.Interfaces;
using Uptec.Erp.Producao.Domain.Pecas.Models;
using Uptec.Erp.Shared.Domain.Enums;

namespace Uptec.Erp.Producao.Domain.Fiscal.Specifications
{
    class NotaEntradaFornecedorPecaExistsSpec : IDomainSpecification<NotaEntrada>
    {
        private readonly IPecaRepository _pecaRepository;
        private readonly NotaEntradaItens _item;
        private readonly Fornecedor _fornecedor;

        public NotaEntradaFornecedorPecaExistsSpec(IPecaRepository pecaRepository, NotaEntradaItens item, Fornecedor fornecedor)
        {
            _pecaRepository = pecaRepository;
            _item = item;
            _fornecedor = fornecedor;
        }

        public bool IsSatisfiedBy(NotaEntrada entity)
        {
            if (_item == null || _fornecedor == null) return true;

            var peca = _pecaRepository.GetByCodigoFornecedor(_fornecedor.Id, _item.Codigo);

            if (peca == null) return false;

            _item.SetCodigoCliente(peca.Codigo);

            return true;
        }
    }
}

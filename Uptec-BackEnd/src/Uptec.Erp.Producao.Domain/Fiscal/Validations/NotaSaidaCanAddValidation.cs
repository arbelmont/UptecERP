using Definitiva.Shared.Domain.DomainValidator;
using Uptec.Erp.Producao.Domain.Clientes.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Specifications;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;
using Uptec.Erp.Producao.Domain.Lotes.Interfaces;
using Uptec.Erp.Shared.Domain.Enums;
using Uptec.Erp.Shared.Domain.Models.Cfop;
using Uptec.Erp.Shared.Domain.Models.Endereco;

namespace Uptec.Erp.Producao.Domain.Fiscal.Validations
{
    public class NotaSaidaCanAddValidation : DomainValidator<NotaSaida>
    {
        public NotaSaidaCanAddValidation(NotaSaida nota, Cliente cliente, Fornecedor fornecedor, 
                                         Endereco endereco, INotaSaidaRepository notaSaidaRepository, ILoteRepository loteRepository)
        {
            if(nota.TipoDestinatario == TipoDestinatario.Cliente)
            {
                Add(new NotaSaidaClienteExistsSpec(cliente), "Cliente n�o encontrado");
                Add(new NotaSaidaClienteEnderecoExistsSpec(cliente, endereco), "Endere�o n�o encontrado ou n�o pertence ao cliente");

                foreach (var item in nota.Itens)
                {
                    if (CfopUptec.CfopsSaidaServico.Contains(item.Cfop))
                        Add(new NotaSaidaLotesHasCoberturaSpec(item, loteRepository), $"Lote {item.LoteNumero} sem cobertura fiscal");
                }
            }
            else
            {
                Add(new NotaSaidaFornecedorExistsSpec(fornecedor), "Fornecedor n�o encontrado");
                Add(new NotaSaidaFornecedorEnderecoExistsSpec(fornecedor, endereco), "Endere�o n�o encontrado ou n�o pertence ao fornecedor");
            }

            foreach (var item in nota.Itens)
            {
                Add(new NotaSaidaItenCfopValidoSpec(item), $"Cfop {item.Cfop} do item {item.Descricao} é inválido.");
            }

            Add(new NotaSaidaHasItensSpec(), "Itens n�o identificados para emiss�o da nota");
            //Add(new NotaSaidaNumeroUnicoSpec(notaSaidaRepository, DomainOperation.Add), "Nota j� cadastrada");
        }
    }
}
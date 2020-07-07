using ExpressMapper;
using ExpressMapper.Extensions;
using Uptec.Erp.Api.ViewModels.Producao.Fornecedores;
using Uptec.Erp.Producao.Domain.Fornecedores.Models;

namespace Uptec.Erp.Api.Mappers.Producao.Map
{
    public class FornecedorMapper
    {
        public FornecedorMapper()
        {
            Mapper.Register<Fornecedor, FornecedorViewModel>()
                    .Member(dst => dst.Cnpj, src => src.Cnpj.NumeroFormatado)
                    .Member(dst => dst.Email, src => src.Email.EnderecoEmail);

            Mapper.Register<FornecedorViewModel, Fornecedor>()
                    .Instantiate(src => new Fornecedor(src.Id, src.Cnpj, src.InscricaoEstadual, src.RazaoSocial,
                        src.NomeFantasia, src.Email, src.Website, src.Observacoes,
                        src.Endereco.Map<FornecedorEnderecoViewModel, FornecedorEndereco>(),
                        src.Telefone.Map<FornecedorTelefoneViewModel, FornecedorTelefone>()));

            Mapper.Register<FornecedorEndereco, FornecedorEnderecoViewModel>()
                .Member(src => src.Estado, dst => dst.Estado.Sigla)
                .Member(src => src.Cidade, dst => dst.Cidade.Nome);

            Mapper.Register<FornecedorEnderecoViewModel, FornecedorEndereco>()
                     .Instantiate(src => new FornecedorEndereco(src.Id, src.FornecedorId, src.Logradouro, src.Numero, src.Complemento,
                        src.Bairro, src.Cep, src.Cidade, src.Estado, src.Tipo));

            Mapper.Register<FornecedorTelefone, FornecedorTelefoneViewModel>();
            Mapper.Register<FornecedorTelefoneViewModel, FornecedorTelefone>()
                .Instantiate(src => new FornecedorTelefone(src.Id, src.FornecedorId, src.Numero, src.Tipo, src.Whatsapp,
                    src.Observacoes, src.Contato));
        }
    }
}
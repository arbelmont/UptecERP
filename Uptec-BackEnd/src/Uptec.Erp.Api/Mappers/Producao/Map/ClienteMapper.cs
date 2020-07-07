using ExpressMapper;
using ExpressMapper.Extensions;
using Uptec.Erp.Api.ViewModels.Producao.Clientes;
using Uptec.Erp.Producao.Domain.Clientes.Models;

namespace Uptec.Erp.Api.Mappers.Producao.Map
{
    public class ClienteMapper
    {
        public ClienteMapper()
        {
            Mapper.Register<Cliente, ClienteViewModel>()
                    .Member(dst => dst.Cnpj, src => src.Cnpj.NumeroFormatado)
                    .Member(dst => dst.Email, src => src.Email.EnderecoEmail);

            Mapper.Register<ClienteViewModel, Cliente>()
                    .Instantiate(src => new Cliente(src.Id, src.Cnpj, src.InscricaoEstadual, src.RazaoSocial,
                        src.NomeFantasia, src.Email, src.Website, src.Observacoes,
                        src.Endereco.Map<ClienteEnderecoViewModel, ClienteEndereco>(),
                        src.Telefone.Map<ClienteTelefoneViewModel, ClienteTelefone>(), src.TransportadoraId));

            Mapper.Register<ClienteEndereco, ClienteEnderecoViewModel>()
                .Member(src => src.Estado, dst => dst.Estado.Sigla)
                .Member(src => src.Cidade, dst => dst.Cidade.Nome);

            Mapper.Register<ClienteEnderecoViewModel, ClienteEndereco>()
                     .Instantiate(src => new ClienteEndereco(src.Id, src.ClienteId, src.Logradouro, src.Numero, src.Complemento,
                        src.Bairro, src.Cep, src.Cidade, src.Estado, src.Tipo));

            Mapper.Register<ClienteTelefone, ClienteTelefoneViewModel>();
            Mapper.Register<ClienteTelefoneViewModel, ClienteTelefone>()
                .Instantiate(src => new ClienteTelefone(src.Id, src.ClienteId, src.Numero, src.Tipo, src.Whatsapp,
                    src.Observacoes, src.Contato));
        }
    }
}

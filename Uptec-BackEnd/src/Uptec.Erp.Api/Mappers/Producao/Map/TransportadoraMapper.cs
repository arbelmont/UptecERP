using ExpressMapper;
using ExpressMapper.Extensions;
using Uptec.Erp.Api.ViewModels.Producao.Transportadoras;
using Uptec.Erp.Producao.Domain.Transportadoras.Models;

namespace Uptec.Erp.Api.Mappers.Producao.Map
{
    public class TransportadoraMapper
    {
        public TransportadoraMapper()
        {
            Mapper.Register<Transportadora, TransportadoraViewModel>()
                    .Member(dst => dst.Cnpj, src => src.Cnpj.NumeroFormatado)
                    .Member(dst => dst.Email, src => src.Email.EnderecoEmail);

            Mapper.Register<TransportadoraViewModel, Transportadora>()
                    .Instantiate(src => new Transportadora(src.Id, src.Cnpj, src.InscricaoEstadual, src.RazaoSocial,
                        src.NomeFantasia, src.Email, src.Website, src.TipoEntregaPadrao, src.Observacoes,
                        src.Endereco.Map<TransportadoraEnderecoViewModel, TransportadoraEndereco>(),
                        src.Telefone.Map<TransportadoraTelefoneViewModel, TransportadoraTelefone>()));

            Mapper.Register<TransportadoraEndereco, TransportadoraEnderecoViewModel>()
                .Member(src => src.Estado, dst => dst.Estado.Sigla)
                .Member(src => src.Cidade, dst => dst.Cidade.Nome);

            Mapper.Register<TransportadoraEnderecoViewModel, TransportadoraEndereco>()
                    .Instantiate(src => new TransportadoraEndereco(src.Id, src.TransportadoraId, src.Logradouro, src.Numero, src.Complemento,
                        src.Bairro, src.Cep, src.Cidade, src.Estado, src.Tipo));

            Mapper.Register<TransportadoraTelefone, TransportadoraTelefoneViewModel>();
            Mapper.Register<TransportadoraTelefoneViewModel, TransportadoraTelefone>()
                .Instantiate(src => new TransportadoraTelefone(src.Id, src.TransportadoraId, src.Numero, src.Tipo, src.Whatsapp,
                    src.Observacoes, src.Contato));
        }
    }
}

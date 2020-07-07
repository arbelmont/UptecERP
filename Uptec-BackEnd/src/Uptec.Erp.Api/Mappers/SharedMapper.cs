using ExpressMapper;
using Uptec.Erp.Api.ViewModels.Shared;
using Uptec.Erp.Shared.Domain.ValueObjects;

namespace Uptec.Erp.Api.Mappers
{
    public class SharedMapper
    {
        public SharedMapper()
        {
            Mapper.Register<Estado, EstadoViewModel>();
            Mapper.Register<Cidade, CidadeViewModel>();
        }
    }
}
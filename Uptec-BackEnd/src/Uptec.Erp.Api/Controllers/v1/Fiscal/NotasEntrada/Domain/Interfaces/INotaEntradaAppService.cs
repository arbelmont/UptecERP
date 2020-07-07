using Uptec.Erp.Api.ViewModels.Producao.NotasEntrada;

namespace Uptec.Erp.Api.Controllers.v1.Fiscal.NotasEntrada.Domain.Interfaces
{
    public interface INotaEntradaAppService
    {
        NotaEntradaViewModel EntrairDadosNotaEntrada(string arquivoXmlNfe);
    }
}

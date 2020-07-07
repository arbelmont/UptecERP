using Uptec.Erp.Producao.Domain.Fiscal.Models;
using Uptec.Erp.Producao.Domain.Fiscal.Models.Dtos;
using Uptec.Erp.Shared.Domain.Models.Impostos;

namespace Uptec.Erp.Producao.Domain.Fiscal.Interfaces
{
    public interface INotaSaidaEmissao
    {
        NotaSaida EmitirNota(NotaSaidaAddDto dto);
        AliquotaImpostos GetAliquotaImpostos(string uf);
        NotaSaidaDestinatario GetDestinatario();
        string GetOutrasInformacoes(NotaSaidaAddDto dto);
    }
}

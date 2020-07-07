using Microsoft.Extensions.DependencyInjection;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces;
using Uptec.Erp.Producao.Domain.Fiscal.Interfaces.Integracao;
using Uptec.Erp.Producao.Infra.Integracao.NFe.Services;

namespace Uptec.Erp.Producao.Infra.Integracao.NFe.IoC
{
    public class IntegracaoNfeIoC
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<INotaFiscalIntegracaoService, NotaFiscalIntegracaoService>();
            services.AddScoped<INotaFiscalSaidaIntegracao, NotaFiscalIntegracaoService>();
            services.AddScoped<INotaFiscalIntegracaoManifestacao, NotaFiscalIntegracaoService>();
        }
    }
}

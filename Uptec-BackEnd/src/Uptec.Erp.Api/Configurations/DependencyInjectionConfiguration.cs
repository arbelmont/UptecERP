using Definitiva.Shared.Infra.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Uptec.Erp.Api.Mappers;
using Uptec.Erp.Producao.Infra.IoC;

namespace Uptec.Erp.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddIoCConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Definitiva.Shared
            DefinitivaSharedBootStraper.RegisterServices(services);

            //Producao
            ProducaoBootStrapper.RegisterServices(services);

            //Mappers
            services.AddSingleton(new ProducaoMapper());
            services.AddSingleton(new SharedMapper());
        }
    }
}

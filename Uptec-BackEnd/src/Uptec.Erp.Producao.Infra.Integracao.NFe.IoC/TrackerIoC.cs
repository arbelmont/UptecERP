using Definitiva.Shared.Infra.Support.Tracker.Domain.Interfaces;
using Definitiva.Shared.Infra.Support.Tracker.Domain.Repository;
using Definitiva.Shared.Infra.Support.Tracker.Domain.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Uptec.Erp.Producao.Infra.Integracao.NFe.IoC
{
    public class TrackerIoC
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ITrackerRepository, TrackerRepository>();
            services.AddScoped<ITrackerService, TrackerService>();
        }
    }
}

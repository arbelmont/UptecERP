using Definitiva.Shared.Domain.Bus;
using Definitiva.Shared.Domain.DomainNotifications;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Definitiva.Shared.Infra.IoC
{
    public static class DefinitivaSharedBootStraper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IBus, Bus>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
        }
    }
}

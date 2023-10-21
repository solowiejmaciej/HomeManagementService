using System.Net.NetworkInformation;
using ReportingServiceWorker.Interfaces;
using ReportingServiceWorker.Services;
using ReportingServiceWorker.Workers;

namespace ReportingServiceWorker.Extensions;


public static class PingServiceCollectionExtension
{
    public static void AddPingServiceCollection(this IServiceCollection services)
    {
        services.AddHostedService<PingWorker>();
        services.AddSingleton<IPingService, PingService>();
        services.AddSingleton<Ping>();
    }
}

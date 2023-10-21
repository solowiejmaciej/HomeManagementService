using ReportingServiceWorker.Factories;
using ReportingServiceWorker.Interfaces;
using ReportingServiceWorker.Models.Clients;
using ReportingServiceWorker.Services;

namespace ReportingServiceWorker.Extensions;

public static class HueServiceCollectionExtension
{
    public static void AddHueServiceCollection(this IServiceCollection services)
    {
        services.AddScoped<IHueClientFactory, HueClientFactory>();
        services.AddScoped<IHueService, HueService>();
    }
}
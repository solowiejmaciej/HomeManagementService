using EasyWakeOnLan;
using ReportingServiceWorker.Services;

namespace ReportingServiceWorker.Extensions;

public static class WoLServiceCollectionExtension
{
    public static void AddWoLServiceCollection(this IServiceCollection services)
    {
        services.AddScoped<IEasyWakeOnLanCient, EasyWakeOnLanClient>();
        services.AddScoped<IWolService, WolService>();
    }
}
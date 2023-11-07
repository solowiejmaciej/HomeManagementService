#region

using System.Net.NetworkInformation;
using HomeManagementService.Interfaces;
using HomeManagementService.Services;
using HomeManagementService.Workers;

#endregion

namespace HomeManagementService.Extensions;

public static class PingServiceCollectionExtension
{
    public static void AddPingServiceCollection(this IServiceCollection services)
    {
        services.AddHostedService<PingWorker>();
        services.AddHostedService<MqttKnobWorker>();
        services.AddSingleton<IPingService, PingService>();
        services.AddSingleton<Ping>();
    }
}
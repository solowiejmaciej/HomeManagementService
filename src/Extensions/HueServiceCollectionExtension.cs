#region

using HomeManagementService.Factories;
using HomeManagementService.Interfaces;
using HomeManagementService.Services;

#endregion

namespace HomeManagementService.Extensions;

public static class HueServiceCollectionExtension
{
    public static void AddHueServiceCollection(this IServiceCollection services)
    {
        services.AddSingleton<IHueClientFactory, HueClientFactory>();
        services.AddSingleton<IHueService, HueService>();
    }
}
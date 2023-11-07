#region

using EasyWakeOnLan;
using HomeManagementService.Interfaces;
using HomeManagementService.Services;

#endregion

namespace HomeManagementService.Extensions;

public static class WoLServiceCollectionExtension
{
    public static void AddWoLServiceCollection(this IServiceCollection services)
    {
        services.AddScoped<IEasyWakeOnLanCient, EasyWakeOnLanClient>();
        services.AddScoped<IWolService, WolService>();
    }
}
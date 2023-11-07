#region

using System.Reflection;
using HomeManagementService.Interfaces;
using HomeManagementService.Models.Options;
using HomeManagementService.Services;

#endregion

namespace HomeManagementService.Extensions;

public static class GeneralServiceCollectionExtension
{
    public static void AddGeneralServiceCollection(this IServiceCollection services)
    {
        services.Configure<HostOptions>(options =>
        {
            options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
            options.ShutdownTimeout = TimeSpan.Zero;
        });
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddOptions<Devices>().BindConfiguration("Devices");
        services.AddOptions<AuthOptions>().BindConfiguration("AuthOptions");
        services.AddOptions<HueOptions>().BindConfiguration("HueOptions");
        services.AddScoped<IDeviceService, DeviceService>();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
}
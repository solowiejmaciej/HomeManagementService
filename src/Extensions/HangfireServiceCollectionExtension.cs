#region

using Hangfire;
using HomeManagementService.Hangfire;

#endregion

namespace HomeManagementService.Extensions;

public static class HangfireServiceCollectionExtension
{
    public static void AddHangfireServiceCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("Hangfire")));

        services.AddHangfireServer((serviceProvider, bjsOptions) =>
        {
            bjsOptions.ServerName = "BarcodeServiceServer";
            bjsOptions.Queues = new[]
            {
                HangfireQueues.HIGH_PRIORITY,
                HangfireQueues.MEDIUM_PRIORITY,
                HangfireQueues.LOW_PRIORITY,
                HangfireQueues.DEFAULT
            };
        });
    }

    public static IApplicationBuilder UseHangfire(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions
        {
            DashboardTitle = "HMS"
        });

        return app;
    }
}
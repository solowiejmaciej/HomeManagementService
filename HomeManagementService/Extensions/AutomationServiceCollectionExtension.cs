#region

using HomeManagementService.Factories;
using HomeManagementService.Hangfire.Manager;
using HomeManagementService.Interfaces;
using HomeManagementService.Models;
using HomeManagementService.Repositories;
using HomeManagementService.Services;

#endregion

namespace HomeManagementService.Extensions;

public static class AutomationServiceCollectionExtension
{
    public static void AddAutomationServiceCollection(this IServiceCollection services)
    {
        services.AddScoped<IAutomationExecutor, AutomationExecutor>();
        services.AddScoped<IRequestFactory, RequestFactory>();
        services.AddScoped<IBackgroundJobManager, BackgroundJobManager>();

        services.AddDbContext<AutomationsDbContext>();
        services.AddScoped<IAutomationRepository, AutomationRepository>();
    }
}
#region

using HomeManagementService.Factories;
using HomeManagementService.Hangfire.Manager;
using HomeManagementService.Interfaces;
using HomeManagementService.Models;
using HomeManagementService.Repositories;
using HomeManagementService.Services;
using Microsoft.EntityFrameworkCore;

#endregion

namespace HomeManagementService.Extensions;

public static class AutomationServiceCollectionExtension
{
    public static void AddAutomationServiceCollection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAutomationExecutor, AutomationExecutor>();
        services.AddScoped<IRequestFactory, RequestFactory>();
        services.AddScoped<IBackgroundJobManager, BackgroundJobManager>();

        services.AddDbContext<AutomationsDbContext>(options =>
            options.UseCosmos(
                configuration["AzureCosmosDb:EndpointUri"],
                configuration["AzureCosmosDb:PrimaryKey"],
                configuration["AzureCosmosDb:DatabaseName"]
            ));
        
        
        services.AddScoped<IAutomationRepository, AutomationRepository>();
    }
}
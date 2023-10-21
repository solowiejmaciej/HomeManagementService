using ReportingServiceWorker.Auth;

namespace ReportingServiceWorker.Extensions;

public static class AuthServiceCollectionExtension
{
    public static void AddAuthServiceCollection(this IServiceCollection services)
    {
        services.AddTransient<IApiKeyValidation, ApiKeyValidation>(); 
        services.AddScoped<ApiKeyAuthFilter>(); 
        services.AddHttpContextAccessor();
    }
}
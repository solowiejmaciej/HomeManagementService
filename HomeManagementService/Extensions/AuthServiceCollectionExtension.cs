#region

using HomeManagementService.Auth;

#endregion

namespace HomeManagementService.Extensions;

public static class AuthServiceCollectionExtension
{
    public static void AddAuthServiceCollection(this IServiceCollection services)
    {
        services.AddTransient<IApiKeyValidation, ApiKeyValidation>();
        services.AddScoped<ApiKeyAuthFilter>();
        services.AddHttpContextAccessor();
    }
}
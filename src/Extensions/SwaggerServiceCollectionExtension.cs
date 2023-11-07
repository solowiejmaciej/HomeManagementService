#region

using Microsoft.OpenApi.Models;

#endregion

namespace HomeManagementService.Extensions;

public static class SwaggerServiceCollectionExtension
{
    public static void AddSwaggerServiceCollection(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeManagmentService", Version = "v1" });

            c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "ApiKey must appear in header",
                Type = SecuritySchemeType.ApiKey,
                Name = "X-Api-Key",
                In = ParameterLocation.Header,
                Scheme = "ApiKeyScheme"
            });

            var apiKeySecurityScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                In = ParameterLocation.Header
            };
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { apiKeySecurityScheme, new string[] { } }
            });
        });
    }
}
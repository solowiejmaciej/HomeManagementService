#region

using HomeManagementService.Models.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

#endregion

namespace HomeManagementService.Auth;

public class ApiKeyAuthFilter : IAuthorizationFilter
{
    private readonly IApiKeyValidation _apiKeyValidation;
    private readonly IOptions<AuthOptions> _authOptions;

    public ApiKeyAuthFilter(
        IApiKeyValidation apiKeyValidation,
        IOptions<AuthOptions> authOptions
    )
    {
        _apiKeyValidation = apiKeyValidation;
        _authOptions = authOptions;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userApiKey = context.HttpContext.Request.Headers[_authOptions.Value.HeaderName].ToString();
        if (string.IsNullOrWhiteSpace(userApiKey))
        {
            context.Result = new BadRequestResult();
            return;
        }

        if (!_apiKeyValidation.IsValidApiKey(userApiKey))
            context.Result = new UnauthorizedResult();
    }
}
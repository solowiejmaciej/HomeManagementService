#region

#endregion

#region

using HomeManagementService.Models.Options;
using Microsoft.Extensions.Options;

#endregion

namespace HomeManagementService.Auth;

public class ApiKeyValidation : IApiKeyValidation
{
    private readonly IOptions<AuthOptions> _authOptions;

    public ApiKeyValidation(IOptions<AuthOptions> authOptions)
    {
        _authOptions = authOptions;
    }

    public bool IsValidApiKey(string userApiKey)
    {
        return userApiKey == _authOptions.Value.ApiKey;
    }
}

public interface IApiKeyValidation
{
    bool IsValidApiKey(string userApiKey);
}
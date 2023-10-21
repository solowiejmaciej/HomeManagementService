#region

#endregion

using Microsoft.Extensions.Options;
using ReportingServiceWorker.Models.Options;

namespace ReportingServiceWorker.Auth;

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
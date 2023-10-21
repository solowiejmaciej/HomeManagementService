#region

using Microsoft.AspNetCore.Mvc;

#endregion

namespace ReportingServiceWorker.Auth;

public class ApiKeyAttribute : ServiceFilterAttribute
{
    public ApiKeyAttribute()
        : base(typeof(ApiKeyAuthFilter))
    {
    }
}
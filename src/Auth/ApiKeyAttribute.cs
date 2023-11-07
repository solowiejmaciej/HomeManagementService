#region

using Microsoft.AspNetCore.Mvc;

#endregion

namespace HomeManagementService.Auth;

public class ApiKeyAttribute : ServiceFilterAttribute
{
    public ApiKeyAttribute()
        : base(typeof(ApiKeyAuthFilter))
    {
    }
}
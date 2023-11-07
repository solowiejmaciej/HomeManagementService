#region

using HomeManagementService.Models;

#endregion

namespace HomeManagementService.Interfaces;

public interface IRequestFactory
{
    Task<HttpRequestMessage> CreateRequestAsync(ActionRequest actionRequest);
}
#region

using HomeManagementService.Interfaces;
using HomeManagementService.Models;
using Microsoft.AspNetCore.WebUtilities;

#endregion

namespace HomeManagementService.Factories;

public class RequestFactory : IRequestFactory
{
    public Task<HttpRequestMessage> CreateRequestAsync(ActionRequest actionRequest)
    {
        var request = new HttpRequestMessage(new HttpMethod(actionRequest.Method), actionRequest.Url);
        foreach (var header in actionRequest.Headers) request.Headers.Add(header.Key, header.Value);

        if (actionRequest.QueryParams != null)
            request.RequestUri = new Uri(QueryHelpers.AddQueryString(actionRequest.Url, actionRequest.QueryParams!));

        if (actionRequest.Body != null) request.Content = new StringContent(actionRequest.Body);
        return Task.FromResult(request);
    }
}
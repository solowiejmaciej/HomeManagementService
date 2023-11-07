#region

using HomeManagementService.Interfaces;
using HomeManagementService.Models;

#endregion

namespace HomeManagementService.Services;

public class AutomationExecutor : IAutomationExecutor
{
    private readonly IRequestFactory _requestFactory;
    private readonly ILogger<AutomationExecutor> _logger;

    public AutomationExecutor(
        IRequestFactory requestFactory, ILogger<AutomationExecutor> logger)
    {
        _requestFactory = requestFactory;
        _logger = logger;
    }

    public async Task<int> Execute(Automation automation)
    {
        _logger.LogInformation($"Executing automation {automation.Id}");
        foreach (var action in automation.ActionRequest)
        {
            var request = await _requestFactory.CreateRequestAsync(action);
            var client = new HttpClient();
            var response = client.SendAsync(request).Result;
            _logger.LogInformation($"Automation {automation.Id} executed with response {response.StatusCode}");
            _logger.LogInformation(response.Content.ReadAsStringAsync().Result);
        }

        return 0;
    }
}

public interface IAutomationExecutor
{
    Task<int> Execute(Automation automation);
}
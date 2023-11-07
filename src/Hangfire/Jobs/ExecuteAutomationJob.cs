#region

#endregion

#region

using Hangfire;
using Hangfire.Server;
using HomeManagementService.Models;
using HomeManagementService.Repositories;
using HomeManagementService.Services;

#endregion

namespace HomeManagementService.Hangfire.Jobs;

public sealed class ExecuteAutomationJob
{
    private readonly IAutomationRepository _repository;
    private readonly ILogger<ExecuteAutomationJob> _logger;
    private readonly IAutomationExecutor _automationExecutor;

    public ExecuteAutomationJob(
        IAutomationRepository repository,
        ILogger<ExecuteAutomationJob> logger,
        IAutomationExecutor automationExecutor
    )
    {
        _repository = repository;
        _logger = logger;
        _automationExecutor = automationExecutor;
    }

    [AutomaticRetry(Attempts = HangfireRetryAttempts.DEFAULT)]
    [JobDisplayName("Execute automation job {0}")]
    [Queue(HangfireQueues.DEFAULT)]
    public async Task Invoke(
        Automation? automation,
        PerformContext context,
        CancellationToken cancellationToken)
    {
        await _automationExecutor.Execute(automation);
    }
}
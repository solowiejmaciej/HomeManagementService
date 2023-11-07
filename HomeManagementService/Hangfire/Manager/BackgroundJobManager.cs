#region

using Hangfire;
using Hangfire.Common;
using HomeManagementService.Hangfire.Jobs;
using HomeManagementService.Models;

#endregion

namespace HomeManagementService.Hangfire.Manager;

public class BackgroundJobManager : IBackgroundJobManager
{
    private readonly IRecurringJobManager _recurringJobManager;

    public BackgroundJobManager(IRecurringJobManager recurringJobManager)
    {
        _recurringJobManager = recurringJobManager;
    }

    public Task ScheduleAsync(Automation? automation, CancellationToken cancellationToken)
    {
        var options = new RecurringJobOptions
        {
            TimeZone = TimeZoneInfo.Local,
            QueueName = HangfireQueues.DEFAULT
        };
        _recurringJobManager.AddOrUpdate(
            automation.Id.ToString(),
            Job.FromExpression<ExecuteAutomationJob>(x => x.Invoke(automation, default!, default!)),
            automation.Cron,
            options
        );
        return Task.CompletedTask;
    }

    public void Remove(string id)
    {
        _recurringJobManager.RemoveIfExists(id);
    }
}

public interface IBackgroundJobManager
{
    Task ScheduleAsync(Automation? automation, CancellationToken cancellationToken);
    void Remove(string id);
}
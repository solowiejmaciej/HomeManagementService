#region

using FluentValidation;
using HomeManagementService.Hangfire.Manager;
using HomeManagementService.Models;
using HomeManagementService.Repositories;
using MediatR;

#endregion

namespace HomeManagementService.Handlers;

public class AddNewAutomationCommandHandler : IRequestHandler<AddAutomationCommand>
{
    private readonly IAutomationRepository _automationRepository;
    private readonly IBackgroundJobManager _backgroundJobManager;

    public AddNewAutomationCommandHandler(
        IAutomationRepository automationRepository,
        IBackgroundJobManager backgroundJobManager
    )
    {
        _automationRepository = automationRepository;
        _backgroundJobManager = backgroundJobManager;
    }

    public async Task Handle(AddAutomationCommand command, CancellationToken cancellationToken)
    {
        await _automationRepository.Add(command.Automation);
        if (command.Automation.IsTimeBased)
            await _backgroundJobManager.ScheduleAsync(command.Automation, cancellationToken);
        if (command.Automation.IsLocationBased)
        {
        }
    }
}

public record AddAutomationCommand : IRequest
{
    public required Automation? Automation { get; init; }
}

public class AddAutomationCommandValidator : AbstractValidator<AddAutomationCommand>
{
    public AddAutomationCommandValidator()
    {
        RuleFor(e => e.Automation)
            .NotNull();
        RuleFor(e => e.Automation.Name)
            .NotEmpty();
        RuleFor(e => e.Automation.ActionRequest)
            .NotEmpty();

        RuleFor(e => e.Automation.IsTimeBased)
            .NotEmpty()
            .Unless(e => e.Automation.IsLocationBased);

        RuleFor(e => e.Automation.IsLocationBased)
            .NotEmpty()
            .Unless(e => e.Automation.IsTimeBased);

        RuleFor(e => e.Automation.Cron)
            .NotEmpty()
            .Unless(e => e.Automation.IsLocationBased);

        RuleFor(e => e.Automation.TriggerLat)
            .NotEmpty()
            .Unless(e => e.Automation.IsTimeBased);

        RuleFor(e => e.Automation.TriggerLong)
            .NotEmpty()
            .Unless(e => e.Automation.IsTimeBased);
    }
}
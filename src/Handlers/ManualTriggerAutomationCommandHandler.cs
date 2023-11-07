#region

using FluentValidation;
using HomeManagementService.Repositories;
using HomeManagementService.Services;
using MediatR;

#endregion

namespace HomeManagementService.Handlers;

public class ManualTriggerAutomationCommandHandler : IRequestHandler<ManualTriggerAutomationCommand>
{
    private readonly IAutomationExecutor _automationExecutor;
    private readonly IAutomationRepository _automationRepository;

    public ManualTriggerAutomationCommandHandler(
        IAutomationExecutor automationExecutor,
        IAutomationRepository automationRepository
    )
    {
        _automationExecutor = automationExecutor;
        _automationRepository = automationRepository;
    }

    public async Task Handle(ManualTriggerAutomationCommand request, CancellationToken cancellationToken)
    {
        var automation = await _automationRepository.GetById(request.Id);
        if (automation == null) return;
        await _automationExecutor.Execute(automation);
    }
}

public record ManualTriggerAutomationCommand : IRequest
{
    public Guid Id { get; set; }
}

public class ManualTriggerAutomationCommandValidator : AbstractValidator<ManualTriggerAutomationCommand>
{
    public ManualTriggerAutomationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
#region

using FluentValidation;
using HomeManagementService.Exceptions;
using HomeManagementService.Hangfire.Manager;
using HomeManagementService.Repositories;
using MediatR;

#endregion

namespace HomeManagementService.Handlers;

public class RemoveAutomationCommandHandler : IRequestHandler<RemoveAutomationCommand>
{
    private readonly IAutomationRepository _automationRepository;
    private readonly IBackgroundJobManager _backgroundJobManager;

    public RemoveAutomationCommandHandler(
        IAutomationRepository automationRepository,
        IBackgroundJobManager backgroundJobManager
    )
    {
        _automationRepository = automationRepository;
        _backgroundJobManager = backgroundJobManager;
    }

    public async Task Handle(RemoveAutomationCommand request, CancellationToken cancellationToken)
    {
        var automation = await _automationRepository.GetById(request.Id);
        if (automation == null) throw new NotFoundException();

        if (automation.IsTimeBased) _backgroundJobManager.Remove(automation.Id.ToString());

        await _automationRepository.RemoveAsync(request.Id);
    }
}

public record RemoveAutomationCommand : IRequest
{
    public Guid Id { get; init; }
}

public class RemoveAutomationCommandValidator : AbstractValidator<RemoveAutomationCommand>
{
    public RemoveAutomationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
#region

using FluentValidation;
using HomeManagementService.Repositories;
using HomeManagementService.Services;
using MediatR;

#endregion

namespace HomeManagementService.Handlers;

public class LocationCommandHandler : IRequestHandler<LocationCommand>
{
    private readonly IAutomationExecutor _automationExecutor;
    private readonly IAutomationRepository _automationRepository;
    private readonly ILocationService _locationService;
    private readonly ILogger<LocationCommandHandler> _logger;

    public LocationCommandHandler(
        IAutomationExecutor automationExecutor,
        IAutomationRepository automationRepository,
        ILocationService locationService,
        ILogger<LocationCommandHandler> logger
    )
    {
        _automationExecutor = automationExecutor;
        _automationRepository = automationRepository;
        _locationService = locationService;
        _logger = logger;
    }

    public async Task Handle(LocationCommand request, CancellationToken cancellationToken)
    {
        var automations = await _automationRepository.GetByCondition(a => a.IsLocationBased && a.IsEnabled);
        if (automations == null) return;
        foreach (var automation in automations)
        {
            var isClose = _locationService.AreCoordinatesClose(automation.TriggerLat, automation.TriggerLong,
                request.Latitude, request.Longitude);
            _logger.LogInformation($"Automation {automation.Id} is close: {isClose}");
            if (isClose) await _automationExecutor.Execute(automation);
        }
    }
}

public record LocationCommand : IRequest
{
    public string Longitude { get; set; }
    public string Latitude { get; set; }
}

public class LocationCommandValidator : AbstractValidator<LocationCommand>
{
    public LocationCommandValidator()
    {
        RuleFor(x => x.Latitude).NotEmpty();
        RuleFor(x => x.Longitude).NotEmpty();
    }
}
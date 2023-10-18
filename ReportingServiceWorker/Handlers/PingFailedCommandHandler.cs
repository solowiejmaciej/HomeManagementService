using System.Diagnostics;
using MediatR;
using ReportingServiceWorker.Interfaces;
using ReportingServiceWorker.Models;

namespace ReportingServiceWorker.Handlers;

public class PingFailedCommandHandler : IRequestHandler<PingFailedCommand>
{
    private readonly ILogger<PingFailedCommandHandler> _logger;
    private readonly INotificationApiClient _notificationApiClient;

    public PingFailedCommandHandler(
        ILogger<PingFailedCommandHandler> logger,
        INotificationApiClient notificationApiClient
        )
    {
        _logger = logger;
        _notificationApiClient = notificationApiClient;
    }
    public async Task Handle(PingFailedCommand request, CancellationToken cancellationToken)
    {
        //Status change detected
        var device = request.Device;
        _logger.LogInformation($"{DateTime.Now} {device}");
    }
}

public record PingFailedCommand : IRequest
{
    public required Device Device { get; set; }
}

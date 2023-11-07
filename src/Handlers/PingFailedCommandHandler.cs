#region

using HomeManagementService.Models;
using MediatR;

#endregion

namespace HomeManagementService.Handlers;

public class PingFailedCommandHandler : IRequestHandler<PingFailedCommand>
{
    private readonly ILogger<PingFailedCommandHandler> _logger;

    public PingFailedCommandHandler(
        ILogger<PingFailedCommandHandler> logger
    )
    {
        _logger = logger;
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
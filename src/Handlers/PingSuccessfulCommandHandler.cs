#region

using HomeManagementService.Models;
using MediatR;

#endregion

namespace HomeManagementService.Handlers;

public class PingSuccessfulCommandHandler : IRequestHandler<PingSuccessfulCommand>
{
    private readonly ILogger<PingSuccessfulCommandHandler> _logger;

    public PingSuccessfulCommandHandler(
        ILogger<PingSuccessfulCommandHandler> logger
    )
    {
        _logger = logger;
    }

    public async Task Handle(PingSuccessfulCommand request, CancellationToken cancellationToken)
    {
        //Status change detected
        var device = request.Device;
        _logger.LogInformation($"{DateTime.Now} {device}");
    }
}

public class PingSuccessfulCommand : IRequest
{
    public required Device Device { get; set; }
}
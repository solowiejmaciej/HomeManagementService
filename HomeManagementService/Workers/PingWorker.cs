#region

using HomeManagementService.Handlers;
using HomeManagementService.Interfaces;
using HomeManagementService.Models;
using MediatR;

#endregion

namespace HomeManagementService.Workers;

public class PingWorker : BackgroundService
{
    private readonly ILogger<PingWorker> _logger;
    private readonly IPingService _pingService;
    private readonly IMediator _mediator;

    public PingWorker(
        ILogger<PingWorker> logger,
        IPingService pingService,
        IMediator mediator
    )
    {
        _logger = logger;
        _pingService = pingService;
        _mediator = mediator;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            //_logger.LogInformation($"Device State: {DeviceState.Status}");
            var result = await _pingService.PingAsync();
            foreach (var device in result)
                if (device.Status == EDeviceState.Online &&
                    device.Status != device.PreviousStatus) // Status changed from Offline to Online
                    await _mediator.Send(new PingSuccessfulCommand { Device = device }, stoppingToken);
                else if (device.Status == EDeviceState.Offline &&
                         device.Status != device.PreviousStatus) // Status changed from Online to Offline
                    await _mediator.Send(new PingFailedCommand { Device = device }, stoppingToken);
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
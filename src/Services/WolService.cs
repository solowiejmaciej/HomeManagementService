#region

using EasyWakeOnLan;
using HomeManagementService.Interfaces;
using HomeManagementService.Models.Options;
using Microsoft.Extensions.Options;
using Renci.SshNet;

#endregion

namespace HomeManagementService.Services;

public class WolService : IWolService
{
    private readonly IEasyWakeOnLanCient _wolClient;
    private readonly ILogger<WolService> _logger;
    private readonly IOptions<Devices> _device;

    public WolService(
        IEasyWakeOnLanCient wolClient,
        ILogger<WolService> logger, IOptions<Devices> device)
    {
        _wolClient = wolClient;
        _logger = logger;
        _device = device;
    }

    public async Task WakeUpAsync(int id)
    {
        var device = _device.Value.List.FirstOrDefault(d => d.Id == id);
        _logger.LogInformation("Waking device {0}", device.Alias);
        await _wolClient.WakeAsync(device.MacAddress);
        _logger.LogInformation("PC Woken");
    }

    public Task Shutdown(int id)
    {
        var device = _device.Value.List.FirstOrDefault(d => d.Id == id);
        var sshClient = new SshClient(device.IpAddress, device.Username, device.Password);

        _logger.LogInformation("Shutting down PC");
        sshClient.Connect();
        sshClient.RunCommand("shutdown /s /f /t 10");
        sshClient.Disconnect();
        _logger.LogInformation("PC Shut down");
        return Task.CompletedTask;
    }
}
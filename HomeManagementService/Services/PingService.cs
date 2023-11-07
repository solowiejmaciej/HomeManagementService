#region

using HomeManagementService.Interfaces;
using HomeManagementService.Models;
using HomeManagementService.Models.Options;
using Microsoft.Extensions.Options;

#endregion

namespace HomeManagementService.Services;

public class PingService : IPingService
{
    private readonly IOptions<Devices> _devices;

    public PingService(
        IOptions<Devices> devices
    )
    {
        _devices = devices;
    }

    public async Task<List<Device>> PingAsync()
    {
        var devices = _devices.Value.List;
        foreach (var device in devices) await device.PingAsync();

        return devices.ToList();
    }
}
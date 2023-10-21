using System.Net.NetworkInformation;
using Microsoft.Extensions.Options;
using ReportingServiceWorker.Interfaces;
using ReportingServiceWorker.Models;
using ReportingServiceWorker.Models.Options;

namespace ReportingServiceWorker.Services;

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
        foreach (var device in devices)
        {
           await device.PingAsync();
        }

        return devices.ToList();
    }
}
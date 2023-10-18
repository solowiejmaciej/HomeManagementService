using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ReportingServiceWorker.Models;
using ReportingServiceWorker.Models.Options;

namespace ReportingServiceWorker.Controllers;

[ApiController]
[Route("[controller]")]
public class DevicesController : ControllerBase
{
    private readonly IOptions<Devices> _devices;

    public DevicesController(IOptions<Devices> devices)
    {
        _devices = devices;
    }
    
    [HttpGet]
    public Device[] GetDevices()
    {
        return _devices.Value.List;
    }
}
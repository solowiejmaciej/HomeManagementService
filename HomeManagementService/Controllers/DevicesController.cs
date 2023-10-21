using Microsoft.AspNetCore.Mvc;
using ReportingServiceWorker.Interfaces;
using ReportingServiceWorker.Models.Dto;

namespace ReportingServiceWorker.Controllers;

[ApiController]
[Route("[controller]")]
public class DevicesController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DevicesController(
        IDeviceService deviceService
        )
    {
        _deviceService = deviceService;
    }
    
    [ProducesResponseType(typeof(List<DeviceDto>), 200)]
    [HttpGet]
    public List<DeviceDto> GetAll()
    {
        return _deviceService.GetAll().Result;
    }
}
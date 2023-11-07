#region

using HomeManagementService.Interfaces;
using HomeManagementService.Models.Dto;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace HomeManagementService.Controllers;

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
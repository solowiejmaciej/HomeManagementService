using HomeManagementService.Models;
using Microsoft.AspNetCore.Mvc;

namespace HomeManagementService.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly TemperatureInfo _temperatureInfo;

    public WeatherController(
        TemperatureInfo temperatureInfo
        )
    {
        _temperatureInfo = temperatureInfo;
    }

    [ProducesResponseType(typeof(List<>), 200)]
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_temperatureInfo);
    }
}
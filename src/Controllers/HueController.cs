#region

using HomeManagementService.Interfaces;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace HomeManagementService.Controllers;

[ApiController]
[Route("[controller]")]
public class HueController : ControllerBase
{
    private readonly IHueService _hueService;

    public HueController(
        IHueService hueService
    )
    {
        _hueService = hueService;
    }

    [HttpPost("State/TurnOffAsync")]
    public async Task TurnOffAsync()
    {
        await _hueService.TurnOffLightsAsync();
    }

    [HttpPost("State/TurnOffAsync/{lightId:int}")]
    public async Task TurnOffAsync(
        [FromRoute] int lightId
    )
    {
        await _hueService.TurnOffLightAsync(lightId);
    }

    [HttpPost("State/TurnOnAsync")]
    public async Task TurnOnAsync()
    {
        await _hueService.TurnOnLightsAsync();
    }

    [HttpPost("State/TurnOnAsync/{lightId:int}")]
    public async Task TurnOnAsync(
        [FromRoute] int lightId
    )
    {
        await _hueService.TurnOnLightAsync(lightId);
    }

    [HttpPut("State/SetBrightnessAsync")]
    public async Task SetBrightnessAsync(
        [FromQuery] int brightness
    )
    {
        await _hueService.SetBrightnessToAllAsync(brightness);
    }

    [HttpPut("State/SetBrightnessAsync/{lightId:int}")]
    public async Task SetBrightnessAsync(
        [FromQuery] int brightness,
        [FromRoute] int lightId
    )
    {
        await _hueService.SetBrightnessAsync(brightness, lightId);
    }

    [HttpPut("State/SetColor")]
    public async Task SetColorAsync(
        [FromQuery] string color
    )
    {
        await _hueService.SetColorToAllAsync(color);
    }

    [HttpPut("State/SetColor/{lightId:int}")]
    public async Task SetColorAsync(
        [FromQuery] string color,
        [FromRoute] int lightId
    )
    {
        await _hueService.SetColorAsync(color, lightId);
    }
}
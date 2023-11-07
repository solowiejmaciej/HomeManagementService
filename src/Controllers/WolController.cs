#region

using HomeManagementService.Auth;
using HomeManagementService.Interfaces;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace HomeManagementService.Controllers;

[ApiKey]
[ApiController]
[Route("[controller]")]
public class WolController : ControllerBase
{
    private readonly ILogger<WolController> _logger;
    private readonly IWolService _wolService;

    public WolController(
        ILogger<WolController> logger,
        IWolService wolService
    )
    {
        _logger = logger;
        _wolService = wolService;
    }

    [HttpPost("WakeUp/{id:int}")]
    public async Task<IActionResult> WakeUpAsync(int id)
    {
        _logger.LogInformation("WakeOnLan called");
        await _wolService.WakeUpAsync(id);
        return Ok();
    }

    [HttpPost("Shutdown/{id:int}")]
    public async Task<IActionResult> ShutdownPc(int id)
    {
        _logger.LogInformation("Shutdown called");
        await _wolService.Shutdown(id);
        return Ok();
    }
}
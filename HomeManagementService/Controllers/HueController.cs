using Microsoft.AspNetCore.Mvc;
using ReportingServiceWorker.Interfaces;

namespace ReportingServiceWorker.Controllers;

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
    [HttpGet]
    public async Task Get()
    { 
        await _hueService.IsOnAsync();
    }
}
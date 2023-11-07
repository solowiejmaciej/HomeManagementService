#region

using HomeManagementService.Handlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace HomeManagementService.Controllers;

[ApiController]
[Route("[controller]")]
public class AutomationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AutomationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add(
        [FromBody] AddAutomationCommand command
    )
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Remove(
        [FromRoute] Guid id
    )
    {
        await _mediator.Send(new RemoveAutomationCommand { Id = id });
        return Ok();
    }

    [HttpPost("ManualTrigger")]
    public async Task<IActionResult> ManualTrigger(
        [FromQuery] Guid id
    )
    {
        await _mediator.Send(new ManualTriggerAutomationCommand { Id = id });
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var result = await _mediator.Send(new GetAllAutomationsQuery());
        return Ok(result);
    }

    [HttpPost("Location")]
    public async Task<IActionResult> SendLocationUpdate(
        [FromBody] LocationCommand request
    )
    {
        await _mediator.Send(request);
        return Ok();
    }
}
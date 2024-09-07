using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/events")]
public class EventController(
    IEventService _eventService
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _eventService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _eventService.GetByIdAsync(id);

        return Ok(result);
    }

    [HttpPost("services/{serviceId}/plans/{planId}")]
    public async Task<IActionResult> Create([FromRoute] Guid serviceId, [FromRoute] Guid planId, [FromBody] EventCreateRequest request)
    {
        await _eventService.CreateAsync(serviceId, planId, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _eventService.DeleteAsync(id);

        return NoContent();
    }
}

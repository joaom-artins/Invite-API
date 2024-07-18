using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/events/{eventId}/invites/{inviteId}/responsible")]
public class ResponsibleController(
    IResponsibleService _responsibleService
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _responsibleService.GetAll();

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid eventId, [FromRoute] Guid inviteId, [FromRoute] Guid id)
    {
        var result = await _responsibleService.GetById(id, eventId, inviteId);

        return Ok(result);
    }

    [HttpPost]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Create([FromRoute] Guid eventId, [FromRoute] Guid inviteId, [FromBody] ResponsibleCreateRequest request)
    {
        await _responsibleService.CreateAsync(eventId, inviteId, request);

        return NoContent();
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromRoute] Guid eventId, [FromRoute] Guid inviteId, [FromBody] ResponsibleUpdateRequest request)
    {
        await _responsibleService.UpdateAsync(id, eventId, inviteId, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> Delete([FromRoute] Guid eventId, [FromRoute] Guid inviteId, [FromRoute] Guid id)
    {
        await _responsibleService.DeleteAsync(id, eventId, inviteId);

        return NoContent();
    }
}

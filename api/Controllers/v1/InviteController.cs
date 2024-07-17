using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/events/{eventId}/invites")]
public class InviteController(
    IInviteService _inviteService
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> FindByUserAndEvent([FromRoute] Guid eventId)
    {
        var records = await _inviteService.FindByEventAndUser(eventId);

        return Ok(records);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAndEventAndUser([FromRoute] Guid eventId, [FromRoute] Guid id)
    {
        var record = await _inviteService.GetByIdAndEventAndUserAsync(id, eventId);

        return Ok(record);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromRoute] Guid eventId, [FromBody] InviteCreateRequest request)
    {
        await _inviteService.CreateAsync(eventId, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid eventId, [FromRoute] Guid id)
    {
        await _inviteService.DeleteAsync(id, eventId);

        return NoContent();
    }
}

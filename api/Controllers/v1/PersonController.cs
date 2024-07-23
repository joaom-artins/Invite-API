using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/events/{eventId}/invites/{inviteId}/responsibles/{responsibleId}/persons")]
public class PersonController(
    IPersonService _personService
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> FindByResponsible([FromRoute] Guid eventId, [FromRoute] Guid inviteId, [FromRoute] Guid responsibleId)
    {
        var result = await _personService.FindByResponsible(eventId, inviteId, responsibleId);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddToResponsible([FromRoute] Guid eventId, [FromRoute] Guid inviteId, [FromRoute] Guid responsibleId, [FromBody] PersonCreateRequest request)
    {
        await _personService.AddToResponsibleAsync(eventId, inviteId, responsibleId, request);

        return NoContent();
    }

    [HttpDelete("{id}/remove")]
    public async Task<IActionResult> RemoveFromResponbile([FromRoute] Guid eventId, [FromRoute] Guid inviteId, [FromRoute] Guid id, [FromRoute] Guid responsibleId)
    {
        await _personService.RemoveFromResponsibleAsync(eventId, inviteId, responsibleId, id);

        return NoContent();
    }

    [HttpDelete("remove-all")]
    public async Task<IActionResult> RemoveAll([FromRoute] Guid eventId, [FromRoute] Guid inviteId, [FromRoute] Guid responsibleId)
    {
        await _personService.RemoveAll(eventId, inviteId, responsibleId);

        return NoContent();
    }
}
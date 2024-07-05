using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/persons")]
public class PersonController(
    IPersonService _personService
) : ControllerBase
{
    [HttpGet("{responsibleId}")]
    public async Task<IActionResult> FindByResponsible(Guid responsibleId)
    {
        var result = await _personService.FindByResponsible(responsibleId);

        return Ok(result);
    }

    [HttpPost("{responsibleId}/add")]
    public async Task<IActionResult> AddToResponsible(Guid responsibleId, PersonCreateRequest request)
    {
        await _personService.AddToResponsibleAsync(responsibleId, request);

        return NoContent();
    }

    [HttpDelete("{id}/responsible/{responsibleId}/remove")]
    public async Task<IActionResult> RemoveFromResponbile(Guid id, Guid responsibleId)
    {
        await _personService.RemoveFromResponsibleAsync(responsibleId, id);

        return NoContent();
    }
}

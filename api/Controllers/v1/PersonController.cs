using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/responsibles/{responsibleId}/persons")]
public class PersonController(
    IPersonService _personService
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> FindByResponsible(Guid responsibleId)
    {
        var result = await _personService.FindByResponsible(responsibleId);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddToResponsible(Guid responsibleId, PersonCreateRequest request)
    {
        await _personService.AddToResponsibleAsync(responsibleId, request);

        return NoContent();
    }

    [HttpDelete("{id}/remove")]
    public async Task<IActionResult> RemoveFromResponbile(Guid id, Guid responsibleId)
    {
        await _personService.RemoveFromResponsibleAsync(responsibleId, id);

        return NoContent();
    }

    [HttpDelete("remove-all")]
    public async Task<IActionResult> RemoveAll(Guid responsibleId)
    {
        await _personService.RemoveAll(responsibleId);

        return NoContent();
    }
}

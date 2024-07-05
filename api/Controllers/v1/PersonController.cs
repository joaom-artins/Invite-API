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
}

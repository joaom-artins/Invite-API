using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/leads")]
public class LeadController(
    ILeadService _leadService
) : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var records = await _leadService.GetAllAsync();

        return Ok(records);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] LeadCreateRequest request)
    {
        await _leadService.CreateAsync(request);

        return NoContent();
    }
}

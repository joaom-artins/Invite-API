using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/cerimonialists")]
public class CerimonialistController(
    ICerimonialistService _cerimonialistService
) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var result = await _cerimonialistService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _cerimonialistService.GetByIdAsync(id);

        return Ok(result);
    }

    [HttpGet("name")]
    [AllowAnonymous]
    public async Task<IActionResult> SearchByName([FromQuery] string name)
    {
        var result = await _cerimonialistService.SearchByNameAsync(name);

        return Ok(result);
    }

    [HttpPost("services/{serviceId}")]
    public async Task<IActionResult> Create([FromRoute] Guid serviceId, [FromBody] CerimonialistCreateRequest request)
    {
        await _cerimonialistService.CreateAsync(serviceId, request);

        return NoContent();
    }

    [HttpPut("{id}/services/{serviceId}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromRoute] Guid serviceId, [FromBody] CerimonialistUpdateRequest request)
    {
        await _cerimonialistService.UpdateAsync(id, serviceId, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id)
    {
        await _cerimonialistService.RemoveAsync(id);

        return NoContent();
    }
}

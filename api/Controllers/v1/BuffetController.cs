using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/buffets")]
public class BuffetController(
    IBuffetService _buffetService
) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var result = await _buffetService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _buffetService.GetByIdAsync(id);

        return Ok(result);
    }

    [HttpPost("service/{serviceId}")]
    public async Task<IActionResult> Create([FromRoute] Guid serviceId, [FromBody] BuffetCreateRequest request)
    {
        await _buffetService.CreateAsync(serviceId, request);

        return NoContent();
    }

    [HttpPut("{id}/service /{serviceId}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromRoute] Guid serviceId, [FromBody] BuffetUpdateRequest request)
    {
        await _buffetService.UpdateAsync(id, serviceId, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _buffetService.DeleteAsync(id);

        return NoContent();
    }
}

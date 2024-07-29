using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/halls")]
public class HallController(
    IHallService _hallService
) : ControllerBase
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var result = await _hallService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var result = await _hallService.GetByIdAsync(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] HallCreateRequest request)
    {
        await _hallService.CreateAsync(request);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, HallUpdateRequest request)
    {
        await _hallService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _hallService.DeleteAsync(id);

        return NoContent();
    }
}
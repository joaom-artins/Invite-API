using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/responsibles")]
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
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _responsibleService.GetById(id);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ResponsibleCreateRequest request)
    {
        await _responsibleService.CreateAsync(request);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ResponsibleUpdateRequest request)
    {
        await _responsibleService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _responsibleService.DeleteAsync(id);

        return NoContent();
    }
}

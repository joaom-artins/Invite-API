using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("v1/responsible")]
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
        if (result is null)
        {
            return NotFound("Corno não encontrado");
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ResponsibleCreateRequest request)
    {
        var result = await _responsibleService.CreateAsync(request);
        if (!result)
        {
            return BadRequest("Corno não deu certo!!");
        }

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, ResponsibleUpdateRequest request)
    {
        var result = await _responsibleService.UpdateAsync(id, request);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _responsibleService.DeleteAsync(id);
        if (!result)
        {
            return NotFound("Responsável não encontrado!");
        }

        return NoContent();
    }
}

using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/comments")]
public class CommentController(
    ICommentService _commentService
) : ControllerBase
{
    [HttpGet("hall/{hallId}")]
    [AllowAnonymous]
    public async Task<IActionResult> FIndByHall([FromRoute] Guid hallId)
    {
        var result = await _commentService.FindByHallAsync(hallId);

        return Ok(result);
    }

    [HttpGet("buffet/{buffetId}")]
    [AllowAnonymous]
    public async Task<IActionResult> FindByBuffet([FromRoute] Guid buffetId)
    {
        var result = await _commentService.FindByBuffetAsync(buffetId);

        return Ok(result);
    }

    [HttpGet("hall/{hallId}/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAndHall([FromRoute] Guid hallId, [FromRoute] Guid id)
    {
        var result = await _commentService.GetByIdAndHallAsync(id, hallId);

        return Ok(result);
    }

    [HttpGet("buffet/{buffetId}/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAndBuffet([FromRoute] Guid buffetId, [FromRoute] Guid id)
    {
        var result = await _commentService.GetByIdAndBuffetAsync(id, buffetId);

        return Ok(result);
    }

    [HttpPost("hall/{hallId}")]
    public async Task<IActionResult> CreateForHall([FromRoute] Guid hallId, [FromBody] CommentCreateRequest request)
    {
        await _commentService.CreateForHallAsync(hallId, request);

        return NoContent();
    }

    [HttpPost("buffet/{buffetId}")]
    public async Task<IActionResult> CreateForBuffet([FromRoute] Guid buffetId, [FromBody] CommentCreateRequest request)
    {
        await _commentService.CreateForBuffetAsync(buffetId, request);

        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CommentUpdateRequest request)
    {
        await _commentService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        await _commentService.DeleteAsync(id);

        return NoContent();
    }
}

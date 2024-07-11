using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/users")]
public class UserController(
    IUserService _userService
) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] UserCreateRequest request)
    {
        await _userService.CreateAsync(request);

        return NoContent();
    }
}

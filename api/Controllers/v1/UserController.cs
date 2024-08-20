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
    [HttpGet("me")]
    public async Task<IActionResult> GetForLoggedUser()
    {
        var result = await _userService.GetLoggedUserAsync();

        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody] UserCreateRequest request)
    {
        await _userService.CreateAsync(request);

        return NoContent();
    }

    [HttpPatch("update-profile")]
    public async Task<IActionResult> Update([FromBody] UserUpdateProfileRequest request)
    {
        await _userService.UpdateProfileAsync(request);

        return NoContent();
    }

    [HttpPatch("update-password")]
    public async Task<IActionResult> UpdatePassword([FromBody] UserUpdatePasswordRequest request)
    {
        await _userService.UpdatePasswordAsync(request);

        return NoContent();
    }
}

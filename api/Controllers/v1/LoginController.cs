using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Requests;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers.v1;

[ApiController]
[Route("v1/login")]
public class LoginController(
    INotificationContext _notificationContext,
    ILoginService _loginService
) : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _loginService.Login(request);

        if (!_notificationContext.HasNotifications)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddDays(1)
            };
            Response.Cookies.Append("AUTH_TOKEN", result.AccessToken, cookieOptions);
        }

        return Ok(result);
    }

    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] UserRefreshTokenRequest request)
    {
        var result = await _loginService.LoginWithRefreshTokenAsync(request);

        return Ok(result);
    }
}

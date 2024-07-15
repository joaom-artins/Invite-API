using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Invite.Commons;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Entities.Responses;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Invite.Services.v1;

public class LoginService(
    INotificationContext _notificationContext,
    AppSettings _appSettings,
    IUserRepository _userRepository,
    SignInManager<UserModel> _signInManager,
    UserManager<UserModel> _userManager,
    ITokenService _tokenService
) : ILoginService
{
    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var userRecord = await _userRepository.GetByEmail(request.Email);
        if (userRecord is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.User.NotFound
            );
            return default!;
        }

        var result = await _signInManager.CheckPasswordSignInAsync(userRecord, request.Password, false);
        if (!result.Succeeded)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.User.InvaliData
            );
            return default!;
        }

        var accessToken = await GenerateTokenAsync(userRecord);
        if (_notificationContext.HasNotifications)
        {
            return default!;
        }

        return new LoginResponse
        {
            Token = accessToken
        };
    }

    public async Task<string> GenerateTokenAsync(UserModel user)
    {
        var role = await _userManager.GetRolesAsync(user);
        if (role is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status401Unauthorized,
                title: NotificationTitle.Unauthorized,
                detail: NotificationMessage.User.InvaliData
            );
            return default!;
        }

        var tokenHandle = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Jwt.SecretKey);
        var claims = new List<Claim>
        {
            new("userId", user!.Id.ToString()),
            new(ClaimTypes.Role, role.First()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = _tokenService.GenerateAccessToken(claims);

        return tokenHandle.WriteToken(token);
    }
}

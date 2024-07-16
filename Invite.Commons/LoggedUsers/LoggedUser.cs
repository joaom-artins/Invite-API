using System.Security.Claims;
using Invite.Commons.LoggedUsers.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Invite.Commons.LoggedUsers;

public class LoggedUser(
    IHttpContextAccessor _httpContextAccessor
) : ILoggedUser
{
    public Guid GetId()
    {
        return Guid.Parse(_httpContextAccessor.HttpContext?.User.FindFirstValue("userId")!);
    }

    public string GetRole()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role)!;
    }
}

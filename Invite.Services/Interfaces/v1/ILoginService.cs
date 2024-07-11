using Invite.Entities.Requests;
using Invite.Entities.Responses;

namespace Invite.Services.Interfaces.v1;

public interface ILoginService
{
    Task<LoginResponse> Login(LoginRequest request);
}

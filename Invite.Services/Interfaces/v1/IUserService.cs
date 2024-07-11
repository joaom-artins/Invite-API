using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface IUserService
{
    Task<bool> CreateAsync(UserCreateRequest request);
}

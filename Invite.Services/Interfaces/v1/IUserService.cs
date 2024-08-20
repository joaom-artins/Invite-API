using Invite.Entities.Models;
using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface IUserService
{
    Task<UserModel> GetLoggedUserAsync();
    Task<bool> CreateAsync(UserCreateRequest request);
    Task<bool> UpdatePasswordAsync(UserUpdatePasswordRequest request);
}

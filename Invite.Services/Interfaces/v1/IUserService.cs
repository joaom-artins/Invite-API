using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Entities.Responses;

namespace Invite.Services.Interfaces.v1;

public interface IUserService
{
    Task<UserRespose> GetLoggedUserAsync();
    Task<bool> CreateAsync(UserCreateRequest request);
    Task<bool> UpdateProfileAsync(UserUpdateProfileRequest request);
    Task<bool> UpdatePasswordAsync(UserUpdatePasswordRequest request);
}

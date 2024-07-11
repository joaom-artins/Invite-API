using Invite.Entities.Requests;

namespace Invite.Business.Interfaces.v1;

public interface IUserBusiness
{
    Task<bool> ValidateForCreate(UserCreateRequest request);
}

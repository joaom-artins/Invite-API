using Invite.Entities.Requests;

namespace Invite.Business.Interfaces.v1;

public interface IPersonBusiness
{
    Task<bool> ValidateForCreate(PersonCreateRequest request);
}

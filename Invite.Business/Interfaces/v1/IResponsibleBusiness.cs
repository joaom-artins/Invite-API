using Invite.Entities.Requests;

namespace Invite.Business.Interfaces.v1;

public interface IResponsibleBusiness
{
    Task<bool> ValidateForCreateAsync(ResponsibleCreateRequest request);
}

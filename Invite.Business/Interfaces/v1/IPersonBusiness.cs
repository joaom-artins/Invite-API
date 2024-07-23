using Invite.Entities.Requests;

namespace Invite.Business.Interfaces.v1;

public interface IPersonBusiness
{
    Task<bool> ValidateForCreateAsync(Guid eventId, Guid inviteId, Guid responsibleId, PersonCreateRequest request);
}

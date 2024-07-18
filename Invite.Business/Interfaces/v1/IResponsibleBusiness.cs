using Invite.Entities.Models;
using Invite.Entities.Requests;

namespace Invite.Business.Interfaces.v1;

public interface IResponsibleBusiness
{
    Task<InviteModel> ValidateForCreateAsync(Guid eventId, Guid inviteId, ResponsibleCreateRequest request);
}

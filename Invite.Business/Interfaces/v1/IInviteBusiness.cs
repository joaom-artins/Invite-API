using Invite.Entities.Models;

namespace Invite.Business.Interfaces.v1;

public interface IInviteBusiness
{
    Task<bool> ValidateForCreateAsync(Guid eventId, DateOnly limitDate);
    Task<InviteModel> GetForDeleteAsync(Guid id, Guid eventId);
}
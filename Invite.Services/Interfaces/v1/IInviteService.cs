using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface IInviteService
{
    Task<bool> CreateAsync(Guid eventId, InviteCreateRequest request);
}

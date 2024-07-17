using Invite.Entities.Models;
using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface IInviteService
{
    Task<IEnumerable<InviteModel>> FindByEventAndUser(Guid eventId);
    Task<InviteModel> GetByIdAndEventAndUserAsync(Guid id, Guid eventId);
    Task<bool> CreateAsync(Guid eventId, InviteCreateRequest request);
    Task<bool> DeleteAsync(Guid id, Guid eventId);
}

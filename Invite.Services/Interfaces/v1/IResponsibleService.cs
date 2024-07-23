using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Entities.Responses;

namespace Invite.Services.Interfaces.v1;

public interface IResponsibleService
{
    Task<IEnumerable<ResponsibleModel>> FindByEventAsync(Guid eventId);
    Task<ResponsibleResponse> GetById(Guid id, Guid eventId, Guid inviteId);
    Task<bool> CreateAsync(Guid eventId, Guid inviteId, ResponsibleCreateRequest request);
    Task<bool> UpdateAsync(Guid id, Guid eventId, Guid inviteId, ResponsibleUpdateRequest request);
    Task<bool> DeleteAsync(Guid id, Guid eventId, Guid inviteId);
}

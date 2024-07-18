using Invite.Entities.Models;
using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface IResponsibleService
{
    Task<IEnumerable<ResponsibleModel>> GetAll();
    Task<ResponsibleModel> GetById(Guid id, Guid eventId, Guid inviteId);
    Task<bool> CreateAsync(Guid eventId, Guid inviteId, ResponsibleCreateRequest request);
    Task<bool> UpdateAsync(Guid id, Guid eventId, Guid inviteId, ResponsibleUpdateRequest request);
    Task<bool> DeleteAsync(Guid id, Guid eventId, Guid inviteId);
}

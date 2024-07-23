using Invite.Entities.Models;
using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface IPersonService
{
    Task<IEnumerable<PersonModel>> FindByResponsible(Guid eventId, Guid inviteId, Guid responsibleId);
    Task<bool> CreateAsync(Guid eventId, Guid inviteId, Guid responsibleId, PersonCreateRequest request);
    Task<bool> AddToResponsibleAsync(Guid eventId, Guid inviteId, Guid responsibleId, PersonCreateRequest request);
    Task<bool> RemoveFromResponsibleAsync(Guid eventId, Guid inviteId, Guid responsibleId, Guid id);
    Task<bool> RemoveAll(Guid eventId, Guid inviteId, Guid responsibleId);
}

using Invite.Entities.Models;
using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface IPersonService
{
    Task<IEnumerable<PersonModel>> FindByResponsible(Guid responsibleId);
    Task<bool> CreateAsync(Guid responsibleId, PersonCreateRequest request);
    Task<bool> AddToResponsibleAsync(Guid id, PersonCreateRequest request);
}

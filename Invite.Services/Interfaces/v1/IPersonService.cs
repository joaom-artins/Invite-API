using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface IPersonService
{
    Task<bool> CreateAsync(Guid responsibleId, PersonCreateRequest request);
}

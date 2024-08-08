using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Entities.Responses;

namespace Invite.Services.Interfaces.v1;

public interface IEventService
{
    Task<IEnumerable<EventResponse>> GetAllAsync();
    Task<EventResponse> GetByIdAsync(Guid id);
    Task<bool> CreateAsync(Guid planId, EventCreateRequest request);
    Task<bool> DeleteAsync(Guid id);
}

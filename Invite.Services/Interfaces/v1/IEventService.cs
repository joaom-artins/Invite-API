using Invite.Entities.Models;
using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface IEventService
{
    Task<IEnumerable<EventModel>> GetAllAsync();
    Task<EventModel> GetByIdAsync(Guid id);
    Task<bool> CreateAsync(Guid planId, EventCreateRequest request);
    Task<bool> DeleteAsync(Guid id);
}

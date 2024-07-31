using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IEventRepository : IGenericRepository<EventModel>
{
    Task<IEnumerable<EventModel>> FindByUserAsync(Guid userId);
}

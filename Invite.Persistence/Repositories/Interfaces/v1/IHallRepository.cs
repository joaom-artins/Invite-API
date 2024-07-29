using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IHallRepository : IGenericRepository<HallModel>
{
    Task<bool> ExistsByNameAndUserAsync(Guid userId, string name);
}

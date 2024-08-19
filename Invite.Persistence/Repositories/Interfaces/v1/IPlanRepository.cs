using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IPlanRepository : IGenericRepository<PlanModel>
{
    Task<bool> ExistsByNameAsync(string name);
}

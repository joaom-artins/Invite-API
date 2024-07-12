using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IPlanRepository : IGenericRepository<PlanModel>
{
    Task<PlanModel> GetByNameAsync(string name);
    Task<bool> ExistsByNameAsync(string name);
}

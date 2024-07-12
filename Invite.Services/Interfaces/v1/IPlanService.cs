using Invite.Entities.Models;
using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface IPlanService
{
    Task<IEnumerable<PlanModel>> GetAllAsync();
    Task<IEnumerable<PlanModel>> GetByNameAsync(string name);
    Task<bool> CreateAsync(PlanCreateRequest request);
    Task<bool> UpdateAsync(Guid id, PlanUpdateRequest request);
    Task<bool> DeleteAync(Guid id);
}

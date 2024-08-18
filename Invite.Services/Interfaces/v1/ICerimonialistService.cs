using Invite.Entities.Models;
using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface ICerimonialistService
{
    Task<IEnumerable<CerimonialistModel>> GetAllAsync();
    Task<IEnumerable<CerimonialistModel>> SearchByNameAsync(string name);
    Task<CerimonialistModel> GetByIdAsync(Guid id);
    Task<bool> CreateAsync(CerimonialistCreateRequest request);
    Task<bool> UpdateRateAsync(CerimonialistModel cerimonialist);
    Task<bool> UpdateAsync(Guid id, CerimonialistUpdateRequest request);
    Task<bool> RemoveAsync(Guid id);
}

using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Entities.Responses;

namespace Invite.Services.Interfaces.v1;

public interface ICerimonialistService
{
    Task<IEnumerable<CerimonialistReponse>> GetAllAsync();
    Task<IEnumerable<CerimonialistReponse>> SearchByNameAsync(string name);
    Task<CerimonialistReponse> GetByIdAsync(Guid id);
    Task<bool> CreateAsync(CerimonialistCreateRequest request);
    Task<bool> UpdateRateAsync(CerimonialistModel cerimonialist);
    Task<bool> UpdateAsync(Guid id, CerimonialistUpdateRequest request);
    Task<bool> RemoveAsync(Guid id);
}

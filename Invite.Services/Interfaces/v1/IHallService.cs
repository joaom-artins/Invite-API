using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Entities.Responses;

namespace Invite.Services.Interfaces.v1;

public interface IHallService
{
    Task<IEnumerable<HallResponse>> GetAllAsync();
    Task<HallResponse> GetByIdAsync(Guid id);
    Task<bool> CreateAsync(HallCreateRequest request);
    Task<bool> UpdateRateAsync(HallModel hall);
    Task<bool> UpdateAsync(Guid id, HallUpdateRequest request);
    Task<bool> DeleteAsync(Guid id);
}
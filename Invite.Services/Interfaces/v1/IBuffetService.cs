using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Entities.Responses;

namespace Invite.Services.Interfaces.v1;

public interface IBuffetService
{
    Task<IEnumerable<BuffetResponse>> GetAllAsync();
    Task<BuffetResponse> GetByIdAsync(Guid id);
    Task<bool> CreateAsync(Guid serviceId, BuffetCreateRequest request);
    Task<bool> UpdateRateAsync(BuffetModel buffet);
    Task<bool> UpdateAsync(Guid id, Guid serviceId, BuffetUpdateRequest request);
    Task<bool> DeleteAsync(Guid id);
}

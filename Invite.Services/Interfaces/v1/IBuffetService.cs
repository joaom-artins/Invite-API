using Invite.Entities.Models;
using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface IBuffetService
{
    Task<IEnumerable<BuffetModel>> GetAllAsync();
    Task<BuffetModel> GetByIdAsync(Guid id);
    Task<bool> UpdateRateAsync(BuffetModel buffet);
    Task<bool> UpdateAsync(Guid id, BuffetUpdateRequest request);
    Task<bool> CreateAsync(BuffetCreateRequest request);
    Task<bool> DeleteAsync(Guid id);
}

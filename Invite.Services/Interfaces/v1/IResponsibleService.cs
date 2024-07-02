using Invite.Entities.Models;
using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface IResponsibleService
{
    Task<IEnumerable<ResponsibleModel>> GetAll();
    Task<ResponsibleModel> GetById(Guid id);
    Task<bool> CreateAsync(ResponsibleCreateRequest request);
    Task<bool> UpdateAsync(Guid id, ResponsibleUpdateRequest request);
    Task<bool> DeleteAsync(Guid id);
}

using Invite.Entities.Models;
using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface ILeadService
{
    Task<IEnumerable<LeadModel>> GetAllAsync();
    Task<bool> CreateAsync(LeadCreateRequest request);
    Task<bool> RemoveRange(string email);
}

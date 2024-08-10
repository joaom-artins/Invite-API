using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface ILeadService
{
    Task<bool> CreateAsync(LeadCreateRequest request);
}

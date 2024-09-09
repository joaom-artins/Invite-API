using Invite.Entities.Models;

namespace Invite.Services.Interfaces.v1;

public interface IServiceService
{
    Task<bool> CreateAsync(Guid userId);
    Task<bool> UpdateNextDueDateAsync(ServiceModel service, int userDueDay);
}

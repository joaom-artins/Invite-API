using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IInviteRepository : IGenericRepository<InviteModel>
{
    Task<IEnumerable<InviteModel>> FindByEventAndUserAsync(Guid eventId, Guid userId);
    Task<InviteModel> GetByIdAndEventAndUserAsync(Guid id, Guid eventId, Guid userId);
    Task<bool> ExistsByReferenceAsync(string reference);
}

using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IInviteRepository : IGenericRepository<InviteModel>
{
    Task<bool> ExistsByReferenceAsync(string reference);
}

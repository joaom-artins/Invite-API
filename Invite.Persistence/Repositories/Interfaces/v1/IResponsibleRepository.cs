using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IResponsibleRepository : IGenericRepository<ResponsibleModel>
{
    Task<ResponsibleModel> GetByIdAndEventAndInvite(Guid id, Guid eventId, Guid inviteId);
    Task<bool> ExistsByCpf(string cpf);
}

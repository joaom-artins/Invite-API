using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IResponsibleRepository : IGenericRepository<ResponsibleModel>
{
    Task<IEnumerable<ResponsibleModel>> FindByEventAsync(Guid eventId);
    Task<ResponsibleModel> GetByIdAndEventAndInviteAsync(Guid id, Guid eventId, Guid inviteId);
    Task<bool> ExistsByCpfAsync(string cpf);
}

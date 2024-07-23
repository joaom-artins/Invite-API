using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IPersonsRepository : IGenericRepository<PersonModel>
{
    Task<IEnumerable<PersonModel>> GetByEventAndInviteAndResponsibleAsync(Guid eventId, Guid inviteId, Guid responsibleId);
    Task<PersonModel> GetByIdAndResponsible(Guid id, Guid eventId, Guid inviteId, Guid responsibleId);
    Task<bool> ExistsByCPF(string cpf);
}

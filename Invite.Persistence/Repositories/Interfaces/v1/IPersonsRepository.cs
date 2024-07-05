using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IPersonsRepository : IGenericRepository<PersonModel>
{
    Task<IEnumerable<PersonModel>> GetByResponsible(Guid responsibleId);
    Task<bool> ExistsByCPF(string cpf);
}

using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces;

public interface IResponsibleRepository : IGenericRepository<ResponsibleModel>
{
    Task<bool> ExistsByCpf(string cpf);
}

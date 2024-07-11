using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IUserRepository : IGenericRepository<UserModel>
{
    Task<bool> ExistsByCPFAsync(string cpf);
    Task<bool> ExistsByEmail(string email);
    Task<UserModel> GetByEmail(string email);
}

using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IUserRepository : IGenericRepository<UserModel>
{
    Task<IEnumerable<UserModel>> FindByDueDayAsync(int dueDay);
    Task<UserModel> GetByEmail(string email);
    Task<bool> ExistsByCPFAsync(string cpf);
    Task<bool> ExistsByEmail(string email);
}

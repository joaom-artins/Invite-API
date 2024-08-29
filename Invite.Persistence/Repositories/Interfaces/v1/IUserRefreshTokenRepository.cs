using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IUserRefreshTokenRepository : IGenericRepository<UserRefreshTokenModel>
{
    Task<UserRefreshTokenModel> GetByUserIdAsync(Guid userId);
    Task<UserRefreshTokenModel> GetByTokenAsync(string token);
}

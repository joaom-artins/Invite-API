using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface ICodeRepository : IGenericRepository<CodeModel>
{
    Task<CodeModel> GetByUserIdAsync(Guid userId);
}

using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface ICodeRepository : IGenericRepository<CodeModel>
{
    Task<CodeModel?> GetByUserAsync(Guid userId);
    Task<CodeModel?> GetByCodeAndEmailWithUserAsync(string code, string email);
}

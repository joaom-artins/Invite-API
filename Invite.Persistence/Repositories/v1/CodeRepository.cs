using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Repositories.v1;

public class CodeRepository(
    AppDbContext context
) : GenericRepository<CodeModel>(context),
    ICodeRepository
{
    private readonly AppDbContext _context = context;

    public async Task<CodeModel> GetByUserIdAsync(Guid userId)
    {
        var record = await _context.Codes.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == userId);
        if (record is null)
        {
            return default!;
        }

        return record;
    }
}

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

    public async Task<CodeModel?> GetByCodeAndEmailWithUserAsync(string code, string email)
    {
        return await _context.Codes.AsNoTracking().Include(x => x.User).SingleOrDefaultAsync(x => x.Code == code && x.User.Email == email);
    }
}

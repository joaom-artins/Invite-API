using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Repositories.v1;

public class UserRefreshTokenRepository(
    AppDbContext context
) : GenericRepository<UserRefreshTokenModel>(context),
    IUserRefreshTokenRepository
{
    private readonly AppDbContext _context = context;

    public async Task<UserRefreshTokenModel> GetByUserIdAsync(Guid userId)
    {
        var record = await _context.UserRefreshTokens.AsNoTracking().SingleOrDefaultAsync(x => x.UserId == userId);
        if (record is null)
        {
            return default!;
        }

        return record;
    }

    public async Task<UserRefreshTokenModel> GetByTokenAsync(string token)
    {
        var record = await _context.UserRefreshTokens.AsNoTracking().SingleOrDefaultAsync(x => x.Token == token);
        if (record is null)
        {
            return default!;
        }

        return record;
    }
}

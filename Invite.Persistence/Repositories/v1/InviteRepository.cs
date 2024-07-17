using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Repositories.v1;

public class InviteRepository(
    AppDbContext context
) : GenericRepository<InviteModel>(context),
    IInviteRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> ExistsByReferenceAsync(string reference)
    {
        var exists = await _context.Invites.AsNoTracking().SingleOrDefaultAsync(x => x.Reference == reference);
        if (exists is null)
        {
            return false;
        }

        return true;
    }
}
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

    public async Task<IEnumerable<InviteModel>> FindByEventAndUserAsync(Guid eventId, Guid userId)
    {
        var records = await _context.Invites.Where(x => x.EventId == eventId && x.Event.UserId == userId).ToListAsync();

        return records;
    }

    public async Task<InviteModel> GetByIdAndEventAndUserAsync(Guid id, Guid eventId, Guid userId)
    {
        var record = await _context.Invites.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id && x.EventId == eventId && x.Event.UserId == userId);
        if (record is null)
        {
            return default!;
        }

        return record;
    }

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
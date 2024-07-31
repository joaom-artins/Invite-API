using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Repositories.v1;

public class EventRepository(
    AppDbContext context
) : GenericRepository<EventModel>(context),
    IEventRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<EventModel>> FindByUserAsync(Guid userId)
    {
        var records = await _context.Events.Where(x => x.UserId == userId).ToListAsync();

        return records;
    }
}

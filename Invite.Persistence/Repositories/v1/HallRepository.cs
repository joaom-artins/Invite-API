using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Repositories.v1;

public class HallRepository(
    AppDbContext context
) : GenericRepository<HallModel>(context),
    IHallRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> ExistsByNameAndUserAsync(Guid userId, string name)
    {
        var record = await _context.Halls.SingleOrDefaultAsync(x => x.Name == name && x.UserId == userId);
        if (record is null)
        {
            return false;
        }

        return true;
    }
}
using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Repositories.v1;

public class PlanRepository(
    AppDbContext context
) : GenericRepository<PlanModel>(context),
    IPlanRepository
{
    private readonly AppDbContext _context = context;

    public async Task<PlanModel> GetByNameAsync(string name)
    {
        var record = await _context.Plans.SingleOrDefaultAsync(x => x.Name == name);
        if (record is null)
        {
            return default!;
        }

        return record;
    }
    public async Task<bool> ExistsByNameAsync(string name)
    {
        var record = await _context.Plans.SingleOrDefaultAsync(x => x.Name == name);
        if (record is null)
        {
            return false;
        }

        return true;
    }
}

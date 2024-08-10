using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Repositories.v1;

public class LeadRepository(
    AppDbContext context
) : GenericRepository<LeadModel>(context),
    ILeadRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<LeadModel>> GetByEmailAsync(string email)
    {
        var records = await _context.Leads.Where(x => x.Email == email).ToListAsync();

        return records;
    }
}

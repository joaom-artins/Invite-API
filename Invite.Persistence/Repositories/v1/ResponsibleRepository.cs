using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Repositories.v1;

public class ResponsibleRepository(
    AppDbContext context
) : GenericRepository<ResponsibleModel>(context),
    IResponsibleRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<ResponsibleModel>> FindByEventAsync(Guid eventId)
    {
        var records = await _context.Responsibles.Where(x => x.Invite.EventId == eventId).ToListAsync();

        return records;
    }
    public async Task<ResponsibleModel> GetByIdAndEventAndInviteAsync(Guid id, Guid eventId, Guid inviteId)
    {
        var record = await _context.Responsibles.Include(x => x.Persons).SingleOrDefaultAsync(x => x.Id == id && x.Invite.EventId == eventId && x.InviteId == inviteId);
        if (record is null)
        {
            return default!;
        }

        return record;
    }

    public async Task<bool> ExistsByCpfAsync(string cpf)
    {
        var record = await _context.Responsibles.SingleOrDefaultAsync(x => x.CPF == cpf);
        if (record is null)
        {
            return false;
        }

        return true;
    }
}

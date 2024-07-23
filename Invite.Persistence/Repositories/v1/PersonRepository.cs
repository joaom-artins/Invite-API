using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Repositories.v1;

public class PersonRepository(
    AppDbContext context
) : GenericRepository<PersonModel>(context),
    IPersonsRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<PersonModel>> GetByEventAndInviteAndResponsibleAsync(Guid eventId, Guid inviteId, Guid responsibleId)
    {
        var records = await _context.Persons.Where(x => x.Responsible.Invite.EventId == eventId &&
                                                        x.Responsible.InviteId == inviteId &&
                                                        x.ResponsibleId == responsibleId).ToListAsync();

        return records;
    }

    public async Task<PersonModel> GetByIdAndResponsible(Guid id, Guid eventId, Guid inviteId, Guid responsibleId)
    {
        var record = await _context.Persons.SingleOrDefaultAsync(x => x.Id == id &&
                                                                x.Responsible.Invite.EventId == eventId &&
                                                                x.Responsible.InviteId == inviteId &&
                                                                x.ResponsibleId == responsibleId);
        if (record is null)
        {
            return default!;
        }

        return record;
    }

    public async Task<bool> ExistsByCPF(string cpf)
    {
        var exists = await _context.Persons.SingleOrDefaultAsync(x => x.CPF == cpf);
        if (exists is null)
        {
            return false;
        }

        return true;
    }
}
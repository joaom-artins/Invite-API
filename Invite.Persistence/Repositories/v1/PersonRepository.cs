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

    public async Task<IEnumerable<PersonModel>> GetByResponsible(Guid responsibleId)
    {
        var records = await _context.Persons.Where(x => x.ResponsibleId == responsibleId).ToListAsync();

        return records;
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
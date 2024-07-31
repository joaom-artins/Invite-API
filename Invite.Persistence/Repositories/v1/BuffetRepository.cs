using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Repositories.v1;

public class BuffetRepository(
    AppDbContext context
) : GenericRepository<BuffetModel>(context),
    IBuffetRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> ExistsByNameAndCNPJAndPhoneNumberAsync(string name, string cnpj, string phoneNumber)
    {
        var exists = await _context.Buffets.AsNoTracking().SingleOrDefaultAsync(x => x.Name == name || x.CNPJ == cnpj || x.PhoneNumber == phoneNumber);
        if (exists is null)
        {
            return false;
        }

        return true;
    }
}

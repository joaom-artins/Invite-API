using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Repositories.v1;

public class ResponsibleRepository(
    AppDbContext context
) : GenericRepository<ResponsibleModel>(context),
    IResponsibleRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> ExistsByCpf(string cpf)
    {
        var record = await _context.Responsibles.SingleOrDefaultAsync(x => x.CPF == cpf);
        if (record is null)
        {
            return false;
        }

        return true;
    }
}

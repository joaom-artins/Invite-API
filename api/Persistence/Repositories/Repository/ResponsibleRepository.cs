using api.Entities.Models;
using api.Persistence.Context;
using api.Persistence.Repositories.Interfaces;
using Invite.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Persistence.Repositories.Repository;

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

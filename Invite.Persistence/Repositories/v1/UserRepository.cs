using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Repositories.v1;

public class UserRepository(
    AppDbContext context
) : GenericRepository<UserModel>(context),
    IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<bool> ExistsByCPFAsync(string cpf)
    {
        var record = await _context.Users.SingleOrDefaultAsync(x => x.CPF == cpf);
        if (record == null)
        {
            return false;
        }

        return true;
    }

    public async Task<bool> ExistsByEmail(string email)
    {
        var record = await _context.Users.SingleOrDefaultAsync(x => x.Email == email);
        if (record == null)
        {
            return false;
        }

        return true;
    }

}

using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Repositories.v1;

public class CommentRepository(
    AppDbContext context
) : GenericRepository<CommentModel>(context),
    ICommentRepository
{
    private readonly AppDbContext _context = context;

    public async Task<IEnumerable<CommentModel>> FindByHallIdAsync(Guid hallId)
    {
        var records = await _context.Comments.Where(x => x.HallId == hallId).ToListAsync();

        return records;
    }
}
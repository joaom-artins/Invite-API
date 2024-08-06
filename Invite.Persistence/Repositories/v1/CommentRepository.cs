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

    public async Task<IEnumerable<CommentModel>> FindByHallAsync(Guid hallId)
    {
        var records = await _context.Comments.Where(x => x.HallId == hallId).ToListAsync();

        return records;
    }

    public async Task<IEnumerable<CommentModel>> FindByBuffetAsync(Guid buffetId)
    {
        var records = await _context.Comments.Where(x => x.BuffetId == buffetId).ToListAsync();

        return records;
    }

    public async Task<IEnumerable<CommentModel>> FindByCommentAsync(Guid id)
    {
        var records = await _context.Comments.Where(x => x.CommentId == id).ToListAsync();

        return records;
    }

    public async Task<CommentModel> GetByIdAndHallAsync(Guid id, Guid hallId)
    {
        var record = await _context.Comments.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id && x.HallId == hallId);
        if (record is null)
        {
            return default!;
        }

        return record;
    }

     public async Task<CommentModel> GetByIdAndBuffetAsync(Guid id, Guid buffetId)
    {
        var record = await _context.Comments.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id && x.BuffetId == buffetId);
        if (record is null)
        {
            return default!;
        }

        return record;
    }
}
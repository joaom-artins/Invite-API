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
        return await _context.Comments.AsNoTracking().Where(x => x.HallId == hallId).ToListAsync();
    }

    public async Task<IEnumerable<CommentModel>> FindByBuffetAsync(Guid buffetId)
    {
        return await _context.Comments.AsNoTracking().Where(x => x.BuffetId == buffetId).ToListAsync();
    }

    public async Task<IEnumerable<CommentModel>> FindByCerimonialistAsync(Guid cerimonialistId)
    {
        return await _context.Comments.AsNoTracking().Where(x => x.CerimonialistId == cerimonialistId).ToListAsync();
    }

    public async Task<CommentModel?> GetByIdAndHallAsync(Guid id, Guid hallId)
    {
        return await _context.Comments.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id && x.HallId == hallId);
    }

    public async Task<CommentModel?> GetByIdAndBuffetAsync(Guid id, Guid buffetId)
    {
        return await _context.Comments.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id && x.BuffetId == buffetId);
    }

    public async Task<CommentModel?> GetByIdAndCerimonialistAsync(Guid id, Guid cerimonialistId)
    {
        return await _context.Comments.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id && x.CerimonialistId == cerimonialistId);
    }
}
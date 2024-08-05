using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface ICommentRepository : IGenericRepository<CommentModel>
{
    Task<IEnumerable<CommentModel>> FindByHallIdAsync(Guid hallId);
}
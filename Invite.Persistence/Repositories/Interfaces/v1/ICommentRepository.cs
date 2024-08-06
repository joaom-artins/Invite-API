using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface ICommentRepository : IGenericRepository<CommentModel>
{
    Task<IEnumerable<CommentModel>> FindByHallAsync(Guid hallId);
    Task<IEnumerable<CommentModel>> FindByBuffetAsync(Guid buffetId);
    Task<CommentModel> GetByIdAndHallAsync(Guid id, Guid hallId);
    Task<CommentModel> GetByIdAndBuffetAsync(Guid id, Guid buffetId);
}
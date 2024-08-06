using Invite.Entities.Models;
using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface ICommentService
{
    Task<IEnumerable<CommentModel>> FindByHallAsync(Guid hallId);
    Task<IEnumerable<CommentModel>> FindByBuffetAsync(Guid buffetId);
    Task<CommentModel> GetByIdAndHallAsync(Guid id, Guid hallId);
    Task<CommentModel> GetByIdAndBuffetAsync(Guid id, Guid buffetId);
    Task<bool> CreateForHallAsync(Guid hallId, CommentCreateRequest request);
    Task<bool> CreateForBuffetAsync(Guid buffetId, CommentCreateRequest request);
    Task<bool> UpdateAsync(Guid id, CommentUpdateRequest request);
    Task<bool> DeleteAsync(Guid id);
}
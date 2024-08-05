using Invite.Entities.Requests;

namespace Invite.Services.Interfaces.v1;

public interface ICommentService
{
    Task<bool> CreateForHallAsync(Guid hallId, CommentCreateRequest request);
    Task<bool> CreateForBuffetAsync(Guid buffetId, CommentCreateRequest request);
}
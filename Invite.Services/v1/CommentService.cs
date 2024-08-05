using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Services.v1;

public class CommentService(
    INotificationContext _notificationContext,
    IUnitOfWork _unitOfWork,
    IHallRepository _hallRepository,
    ICommentRepository _commentRepository,
    IBuffetRepository _buffetRepository,
    IHallService _hallService,
    IBuffetService _buffetService
) : ICommentService
{
    public async Task<bool> CreateForHallAsync(Guid hallId, CommentCreateRequest request)
    {
        var hall = await _hallRepository.GetByIdAsync(hallId);
        if (hall is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Hall.NotFound
            );
            return false;
        }

        var comment = await CreateAsync(request);
        comment.HallId = hallId;
        _commentRepository.Update(comment);

        await _hallService.UpdateRateAsync(hall);

        return true;
    }

    public async Task<bool> CreateForBuffetAsync(Guid buffetId, CommentCreateRequest request)
    {
        var buffet = await _buffetRepository.GetByIdAsync(buffetId);
        if (buffet is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Hall.NotFound
            );
            return false;
        }

        var comment = await CreateAsync(request);
        comment.BuffetId = buffetId;
        _commentRepository.Update(comment);

        await _buffetService.UpdateRateAsync(buffet);

        return true;
    }

    private async Task<CommentModel> CreateAsync(CommentCreateRequest request)
    {
        var comment = new CommentModel
        {
            Content = request.Content,
            Stars = request.Stars,
        };
        await _commentRepository.AddAsync(comment);
        await _unitOfWork.CommitAsync();

        return comment;
    }
}

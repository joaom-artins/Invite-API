using Invite.Commons.LoggedUsers.Interfaces;
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
    ILoggedUser _loggedUser,
    IUnitOfWork _unitOfWork,
    IHallRepository _hallRepository,
    ICommentRepository _commentRepository,
    IBuffetRepository _buffetRepository,
    IHallService _hallService,
    IBuffetService _buffetService
) : ICommentService
{
    public async Task<IEnumerable<CommentModel>> FindByHallAsync(Guid hallId)
    {
        var records = await _commentRepository.FindByHallAsync(hallId);

        return records;
    }

    public async Task<IEnumerable<CommentModel>> FindByBuffetAsync(Guid buffetId)
    {
        var records = await _commentRepository.FindByBuffetAsync(buffetId);

        return records;
    }

    public async Task<CommentModel> GetByIdAndHallAsync(Guid id, Guid hallId)
    {
        var record = await _commentRepository.GetByIdAndHallAsync(id, hallId);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Comment.NotFound
            );
            return default!;
        }

        return record;
    }

    public async Task<CommentModel> GetByIdAndBuffetAsync(Guid id, Guid buffetId)
    {
        var record = await _commentRepository.GetByIdAndBuffetAsync(id, buffetId);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Comment.NotFound
            );
            return default!;
        }

        return record;
    }

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
        await _unitOfWork.CommitAsync();

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
        await _unitOfWork.CommitAsync();

        await _buffetService.UpdateRateAsync(buffet);

        return true;
    }

    public async Task<bool> UpdateAsync(Guid id, CommentUpdateRequest request)
    {
        var record = await _commentRepository.GetByIdAndUserAsync(id, _loggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Comment.NotFound
            );
            return false;
        }

        record.Content = request.Content;
        record.Stars = request.Stars;
        record.UpdatedAt = DateTime.Now;
        _commentRepository.Update(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var record = await _commentRepository.GetByIdAndUserAsync(id, _loggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Comment.NotFound
            );
            return false;
        }

        _commentRepository.Remove(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    private async Task<CommentModel> CreateAsync(CommentCreateRequest request)
    {
        var comment = new CommentModel
        {
            Content = request.Content,
            Stars = request.Stars,
            UserId = _loggedUser.GetId(),
        };
        await _commentRepository.AddAsync(comment);
        await _unitOfWork.CommitAsync();

        return comment;
    }
}

using Invite.Business.Interfaces.v1;
using Invite.Commons.LoggedUsers.Interfaces;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Models;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Business.v1;

public class InviteBusiness(
    ILoggedUser _loggedUser,
    INotificationContext _notificationContext,
    IEventRepository _eventRepository,
    IInviteRepository _inviteRepository
) : IInviteBusiness
{
    public async Task<bool> ValidateDateAsync(Guid eventId, DateOnly limitDate)
    {
        var eventRecord = await _eventRepository.GetByIdAndUserAsync(eventId, _loggedUser.GetId());
        if (eventRecord is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.Invite.NotFound
            );
            return false;
        }

        if (eventRecord.Date < limitDate)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.Invite.InvalidDateInCreate
            );
            return false;
        }

        if (limitDate < DateOnly.FromDateTime(DateTime.Now))
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.Invite.InvalidDateInCreate
            );
            return false;
        }

        return true;
    }

    public async Task<InviteModel> GetForDeleteAsync(Guid id, Guid eventId)
    {
        var record = await _inviteRepository.GetByIdAndEventAndUserAsync(id, eventId, _loggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Invite.NotFound
            );
            return default!;
        }

        if (record.Acepted)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.Invite.InvitationAccepted
            );
            return default!;
        }

        if (record.LimitDate < DateOnly.FromDateTime(DateTime.Now) && record.Active)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.Invite.InvalidDateInCreate
            );
            return default!;
        }

        return record;
    }
}

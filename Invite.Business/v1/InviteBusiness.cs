using Invite.Business.Interfaces.v1;
using Invite.Commons.LoggedUsers.Interfaces;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Business.v1;

public class InviteBusiness(
    ILoggedUser _loggedUser,
    INotificationContext _notificationContext,
    IEventRepository _eventRepository
) : IInviteBusiness
{
    public async Task<bool> ValidateDateAsync(Guid eventId, DateOnly limitDate)
    {
        var eventRecord = await _eventRepository.GetByIdAndUserAsync(eventId, _loggedUser.GetId());
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        if (eventRecord.Date > limitDate)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Invite.InvalidDate
            );
            return false;
        }

        if (limitDate < DateOnly.FromDateTime(DateTime.Now))
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Invite.InvalidDate
            );
            return false;
        }

        return true;
    }
}

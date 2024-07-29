using Invite.Business.Interfaces.v1;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Business.v1;

public class HallBusiness(
    INotificationContext _notificationContext,
    IHallRepository _hallRepository
) : IHallBusiness
{
    public async Task<bool> ExistsByName(Guid userId, string name)
    {
        var exists = await _hallRepository.ExistsByNameAndUserAsync(userId, name);
        if (exists)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Hall.ExistsName
            );
            return false;
        }

        return true;
    }
}

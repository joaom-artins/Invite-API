using Invite.Business.Interfaces.v1;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Business.v1;

public class PlanBusiness(
    INotificationContext _notificationContext,
    IPlanRepository _planRepository
) : IPlanBusiness
{
    public async Task<bool> ExistsNameAsync(string name)
    {
        var exists = await _planRepository.ExistsByNameAsync(name);
        if (exists)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status409Conflict,
                title: NotificationTitle.Conflict,
                detail: NotificationMessage.Plan.ExistsName
            );
            return false;
        }
        return true;
    }
}

using Invite.Business.Interfaces.v1;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Business.v1;

public class CerimonialistBusiness(
    INotificationContext _notificationContext,
    ICerimonialistRepository _cerimonialistRepository
) : ICerimonialistBusiness
{
    public async Task<bool> CheckExistsByUserAsync(Guid userId)
    {
        var record = await _cerimonialistRepository.FindByUserAsync(userId);
        if (record.Any())
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.Cerimonialist.ExistsByUser
            );
            return false;
        }

        return true;
    }
}

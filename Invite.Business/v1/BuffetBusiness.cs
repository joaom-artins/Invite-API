using Invite.Business.Interfaces.v1;
using Invite.Commons;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Business.v1;

public class BuffetBusiness(
    INotificationContext _notificationContext,
    IBuffetRepository _buffetRepository
) : IBuffetBusiness
{
    public async Task<bool> ValidateForCreateAndUpdateAsync(string name, string cnpj, string phoneNumber)
    {
        var exists = await _buffetRepository.ExistsByNameAndCNPJAndPhoneNumberAsync(name, CleanString.OnlyNumber(cnpj), CleanString.OnlyNumber(phoneNumber));
        if (exists)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status409Conflict,
                title: NotificationTitle.Conflict,
                detail: NotificationMessage.Buffet.DataExists
            );
            return false;
        }

        return true;
    }
}

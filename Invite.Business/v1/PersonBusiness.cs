using Invite.Business.Interfaces.v1;
using Invite.Commons;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Business.v1;

public class PersonBusiness(
    INotificationContext _notificationContext,
    IPersonsRepository _personsRepository
) : IPersonBusiness
{
    public async Task<bool> ValidateForCreateAsync(PersonCreateRequest request)
    {

        var exists = await _personsRepository.ExistsByCPF(CleanString.OnlyNumber(request.CPF));
        if (exists)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status409Conflict,
                title: NotificationTitle.Conflict,
                detail: NotificationMessage.Person.ExistsCPF
            );
            return false;
        }

        return true;
    }
}

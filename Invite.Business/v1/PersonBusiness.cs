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
    IPersonsRepository _personsRepository,
    IResponsibleRepository _responsibleRepository
) : IPersonBusiness
{
    public async Task<bool> ValidateForCreateAsync(Guid eventId, Guid inviteId, Guid responsibleId, PersonCreateRequest request)
    {
        var record = await _responsibleRepository.GetByIdAndEventAndInviteAsync(responsibleId, eventId, inviteId);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Responsible.NotFound
            );
            return false;
        }

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

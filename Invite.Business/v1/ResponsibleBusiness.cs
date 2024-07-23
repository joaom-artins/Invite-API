using Invite.Business.Interfaces.v1;
using Invite.Commons;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Business.v1;

public class ResponsibleBusiness(
    INotificationContext _notificationContext,
    IInviteRepository _inviteRepository,
    IResponsibleRepository _responsibleRepository
) : IResponsibleBusiness
{
    public async Task<InviteModel> ValidateForCreateAsync(Guid eventId, Guid inviteId, ResponsibleCreateRequest request)
    {
        var inviteRecord = await _inviteRepository.GetByEventAndStatusAsync(eventId);
        if (inviteRecord is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Invite.NotFound
            );
            return default!;
        }

        if (request.Persons.Count() != request.PersonInFamily)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.Responsible.PersonsInRequestInvalid
            );
            return default!;
        }

        var exists = await _responsibleRepository.ExistsByCpfAsync(request.CPF);
        if (exists)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status409Conflict,
                title: NotificationTitle.Conflict,
                detail: NotificationMessage.Responsible.ExistsCPF
            );
            return default!;
        }

        return inviteRecord;
    }
}

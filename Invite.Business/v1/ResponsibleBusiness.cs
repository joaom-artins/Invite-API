using Invite.Business.Interfaces.v1;
using Invite.Commons;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Business.v1;

public class ResponsibleBusiness(
    INotificationContext _notificationContext,
    IResponsibleRepository _responsibleRepository
) : IResponsibleBusiness
{
    public async Task<bool> ValidateForCreateAsync(ResponsibleCreateRequest request)
    {
        if (request.Persons.Count() != request.PersonInFamily)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.Responsible.PersonsInRequestInvalid
            );
            return false;
        }

        var cpfIsValid = ValidateCPF.IsValidCpf(CleanString.OnlyNumber(request.CPF));
        if (!cpfIsValid)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.Common.InvalidCPF
            );
            return false;
        }

        cpfIsValid = ValidateCPF.IsCpfFormatValid(request.CPF);
        if (!cpfIsValid)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.Common.InvalidCPF
            );
            return false;
        }

        var exists = await _responsibleRepository.ExistsByCpf(request.CPF);
        if (exists)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status409Conflict,
                title: NotificationTitle.Conflict,
                detail: NotificationMessage.Responsible.ExistsCPF
            );
            return false;
        }

        return true;
    }
}

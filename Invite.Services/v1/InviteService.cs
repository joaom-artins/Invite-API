using Invite.Business.Interfaces.v1;
using Invite.Commons;
using Invite.Commons.LoggedUsers.Interfaces;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Services.v1;

public class InviteService(
    INotificationContext _notificationContext,
    ILoggedUser _loggedUser,
    IUnitOfWork _unitOfWork,
    IInviteRepository _inviteRepository,
    IInviteBusiness _inviteBusiness
) : IInviteService
{
    public async Task<IEnumerable<InviteModel>> FindByEventAndUser(Guid eventId)
    {
        var records = await _inviteRepository.FindByEventAndUserAsync(eventId, _loggedUser.GetId());

        return records;
    }

    public async Task<InviteModel> GetByIdAndEventAndUserAsync(Guid id, Guid eventId)
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

        return record;
    }

    public async Task<bool> CreateAsync(Guid eventId, InviteCreateRequest request)
    {
        await _inviteBusiness.ValidateDateAsync(eventId, request.LimitDate);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        string reference;
        bool exists;
        do
        {
            reference = References.Generate();
            exists = await _inviteRepository.ExistsByReferenceAsync(reference);
        } while (exists);

        var inviteRecord = new InviteModel
        {
            EventId = eventId,
            Message = request.Message,
            LimitDate = request.LimitDate,
            Reference = reference,
            FutureResponsibleId = Guid.NewGuid(),
        };
        await _inviteRepository.AddAsync(inviteRecord);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid eventId)
    {
        var record = await _inviteBusiness.GetForDeleteAsync(id, eventId);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        _inviteRepository.Remove(record);
        await _unitOfWork.CommitAsync();

        return true;
    }
}

using Invite.Business.Interfaces.v1;
using Invite.Commons;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;

namespace Invite.Services.v1;

public class InviteService(
    INotificationContext _notificationContext,
    IUnitOfWork _unitOfWork,
    IInviteRepository _inviteRepository,
    IInviteBusiness _inviteBusiness
) : IInviteService
{
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
            reference = Reference.Generate();
            exists = await _inviteRepository.ExistsByReferenceAsync(reference);
        } while (exists);

        var inviteRecord = new InviteModel
        {
            EventId = eventId,
            Message = request.Message,
            LimitDate = request.LimitDate,
            Reference = reference
        };
        await _inviteRepository.AddAsync(inviteRecord);
        await _unitOfWork.CommitAsync();

        return true;
    }
}

using Invite.Business.Interfaces.v1;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Enums;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Business.v1;

public class EventBusiness(
    INotificationContext _notificationContext,
    IUserRepository _userRepository,
    IPlanRepository _planRepository
) : IEventBusiness
{
    public async Task<bool> ValidatePlanAsync(Guid planId, Guid userId, int guests)
    {
        var userRecord = await _userRepository.GetByIdAsync(userId);
        if (userRecord is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.User.NotFound
            );
            return false;
        }

        var planRecord = await _planRepository.GetByIdAsync(planId);
        if (planRecord is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Plan.NotFound
            );
            return false;
        }

        if ((planRecord.Type == PlanTypeEnum.Buffet && userRecord.TypeClient == ClientTypeEnum.Person) ||
            (planRecord.Type == PlanTypeEnum.Person && userRecord.TypeClient == ClientTypeEnum.Buffet))
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.Plan.TypeError
            );
            return false;
        }

        if (guests > planRecord.MaxGuest)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.Event.GuestIsMoreThanPlan
            );
            return false;
        }

        return true;
    }
}

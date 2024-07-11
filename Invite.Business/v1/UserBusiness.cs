using Invite.Business.Interfaces.v1;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Invite.Business.v1;

public class UserBusiness(
    INotificationContext _notificationContext,
    IUserRepository _userRepository
) : IUserBusiness
{
    public async Task<bool> ValidateForCreate(UserCreateRequest request)
    {
        if (request.Password != request.ConfirmPassword)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.User.DifferentPasswords
            );
            return false;
        }

        var existsEmail = await _userRepository.ExistsByEmail(request.Email);
        if (existsEmail)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.User.EmailExists
            );
            return false;
        }

        var existsCPF = await _userRepository.ExistsByCPFAsync(request.CPF);
        if (existsCPF)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.User.ExistsCPF
            );
            return false;
        }

        return true;
    }
}

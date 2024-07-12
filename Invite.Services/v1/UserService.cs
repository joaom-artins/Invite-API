using Invite.Business.Interfaces.v1;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Invite.Services.v1;

public class UserService(
    INotificationContext _notificationContext,
    IUnitOfWork _unitOfWork,
    IUserBusiness _userBusiness,
    UserManager<UserModel> _userManager
) : IUserService
{
    public async Task<bool> CreateAsync(UserCreateRequest request)
    {
        await _userBusiness.ValidateForCreate(request);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        _unitOfWork.BeginTransaction();

        var result = await _userManager.CreateAsync(new UserModel
        {
            FullName = request.FullName,
            Email = request.Email,
            UserName = request.Email.ToUpper(),
            CPFOrCNPJ = request.CPFOrCNPJ,
        }, request.Password);
        await _unitOfWork.CommitAsync();

        if (!result.Succeeded)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status500InternalServerError,
                title: NotificationTitle.InternalServerError,
                detail: NotificationMessage.User.ErrorInCreate
            );
            return false;
        }

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status500InternalServerError,
                title: NotificationTitle.InternalServerError,
                detail: NotificationMessage.User.NotFound
            );
            return false;
        }

        var role = await _userManager.AddToRoleAsync(user, "User");
        if (!role.Succeeded)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status500InternalServerError,
                title: NotificationTitle.InternalServerError,
                detail: NotificationMessage.User.ErrorInAddRole
            );
            return false;
        }

        await _unitOfWork.CommitAsync(true);

        return true;
    }
}

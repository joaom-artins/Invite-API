using System.Net.Http.Json;
using Invite.Business.Interfaces.v1;
using Invite.Commons;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Dtos;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Invite.Services.v1;

public class UserService(
    INotificationContext _notificationContext,
    IUnitOfWork _unitOfWork,
    AppSettings _appSettings,
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
            CPFOrCNPJ = CleanString.OnlyNumber(request.CPFOrCNPJ),
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

        await CreateInPaymentService(user);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        await _unitOfWork.CommitAsync(true);

        return true;
    }

    public async Task<bool> CreateInPaymentService(UserModel user)
    {
        var body = new
        {
            name = user.FullName,
            cpfCnpj = user.CPFOrCNPJ,
            email = user.Email,
            notificationDisabled = true
        };

        var httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Add("access_token", _appSettings.Asaas.ApiKey);
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Invites");

        var result = await httpClient.PostAsJsonAsync($"{_appSettings.Asaas.ApiUrl}/customers", body);
        if (!result.IsSuccessStatusCode)
        {
            var errorResponse = await result.Content.ReadAsStringAsync();

            if (errorResponse.Contains("invalid_cpfCnpj"))
            {
                _notificationContext.SetDetails(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: NotificationTitle.BadRequest,
                    detail: NotificationMessage.User.InvalidCpf
                );
                return false;
            }

            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status500InternalServerError,
                title: NotificationTitle.InternalServerError,
                detail: NotificationMessage.User.ErrorInCreateInPaymentService
            );
            return false;
        }

        var response = await result.Content.ReadFromJsonAsync<UserCreateInPaymentServiceResponseDto>();
        user.ExternalId = response!.Id;
        await _userManager.UpdateAsync(user);
        await _unitOfWork.CommitAsync();

        return true;
    }
}


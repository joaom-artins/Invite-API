using System.Net.Http.Json;
using System.Text;
using AutoMapper;
using Invite.Business.Interfaces.v1;
using Invite.Commons;
using Invite.Commons.LoggedUsers.Interfaces;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Dtos;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Entities.Responses;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Invite.Services.v1;

public class UserService(
    INotificationContext _notificationContext,
    ILoggedUser _loggedUser,
    IMapper _mapper,
    UserManager<UserModel> _userManager,
    IUnitOfWork _unitOfWork,
    IUserRepository _userRepository,
    AppSettings _appSettings,
    IUserBusiness _userBusiness,
    ICodeService _codeService,
    ICodeRepository _codeRepository,
    ILeadService _leadService
) : IUserService
{
    public async Task<UserRespose> GetLoggedUserAsync()
    {
        var record = await _userRepository.GetByIdAsync(_loggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.User.NotFound
            );
            return default!;
        }

        return _mapper.Map<UserRespose>(record);
    }

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
            CPF = CleanString.OnlyNumber(request.CPF),
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

        await _leadService.RemoveRange(request.Email);

        return true;
    }

    public async Task<bool> CreateInPaymentService(UserModel user)
    {
        var body = new
        {
            name = user.FullName,
            cpfCnpj = user.CPF,
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

    public async Task<bool> UpdateProfileAsync(UserUpdateProfileRequest request)
    {
        var record = await _userRepository.GetByIdAsync(_loggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.User.NotFound
            );
            return false;
        }

        record.FullName = request.FullName;
        record.Email = request.Email;
        _userRepository.Update(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> UpdatePasswordAsync(UserUpdatePasswordRequest request)
    {
        if (request.NewPassword != request.ConfirmNewPassword)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.User.DifferentPasswords
            );
            return false;
        }

        var user = await _userManager.FindByIdAsync(_loggedUser.GetId().ToString());
        if (user is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.User.NotFound
            );
            return false;
        }

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.User.FailInChangePassword
            );
            return false;
        }

        return true;
    }

    public async Task<UserResetPasswordStep1Response> ResetPasswordStep1Async(UserResetPasswordStep1Request request)
    {
        var record = await _userRepository.GetByEmailAsync(request.Email);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.User.NotFound
            );
            return default!;
        }

        var code = await _codeService.CreateAsync(record.Id);
        if (_notificationContext.HasNotifications)
        {
            return default!;
        }

        //TODO: Envia email

        return new UserResetPasswordStep1Response
        {
            Email = record.Email!
        };
    }

    public async Task<UserResetPasswordStep2Response> ResetPasswordStep2Async(UserResetPasswordStep2Request request)
    {
        var codeRecord = await _codeRepository.GetByCodeAndEmailWithUserAsync(request.Code, request.Email);
        if (codeRecord is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.Code.Invalid
            );
            return default!;
        }

        var difference = DateTime.Now - codeRecord.CreatedAt;
        if (difference.Minutes < _appSettings.Code.ResendInMinutes)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.Code.Valid
            );
            return default!;
        }

        var generatedToken = await _userManager.GeneratePasswordResetTokenAsync(codeRecord.User);
        var encodedToken = Encoding.UTF8.GetBytes($"{codeRecord.UserId}.{generatedToken}");
        var token = Convert.ToBase64String(encodedToken);

        return new UserResetPasswordStep2Response
        {
            Hash = token
        };
    }

    public async Task<bool> ResetPasswordStep3Async(UserResetPasswordStep3Request request)
    {
        if (request.NewPassword != request.ConfirmNewPassword)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status400BadRequest,
                title: NotificationTitle.BadRequest,
                detail: NotificationMessage.User.DifferentPasswords
            );
            return false;
        }

        var encodedHash = Convert.FromBase64String(request.Hash);
        var decodedHash = Encoding.UTF8.GetString(encodedHash);
        var splitedHash = decodedHash.Split(".");
        var userId = splitedHash[0];
        var token = splitedHash[1];

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.User.NotFound
            );
            return false;
        }

        var isValidToken = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
        if (!isValidToken)
        {
            _notificationContext.SetDetails(
               statusCode: StatusCodes.Status400BadRequest,
               title: NotificationTitle.BadRequest,
               detail: NotificationMessage.User.InvalidResetToken
            );
            return false;
        }

        var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
        if (!result.Succeeded)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status500InternalServerError,
                title: NotificationTitle.InternalServerError,
                detail: NotificationMessage.User.FailInResetPassword
            );
            return default!;
        }

        return true;
    }
}


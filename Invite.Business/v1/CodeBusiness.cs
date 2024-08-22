using Invite.Business.Interfaces.v1;
using Invite.Commons;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Invite.Business.v1;

public class CodeBusiness(
    INotificationContext _notificationContext,
    AppSettings _appSettings,
    ICodeRepository _codeRepository,
    IUnitOfWork _unitOfWork
) : ICodeBusiness
{
    public async Task<bool> ValidateForCreateAsync(Guid userId)
    {
        var record = await _codeRepository.GetByUserAsync(userId);
        if (record is not null)
        {
            var difference = DateTime.Now - record.CreatedAt;
            if (difference.Minutes < _appSettings.Code.ResendInMinutes)
            {
                _notificationContext.SetDetails(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: NotificationTitle.BadRequest,
                    detail: NotificationMessage.Code.Valid
                );
                return default!;
            }

            _codeRepository.Remove(record);
            await _unitOfWork.CommitAsync();
        }
        return true;
    }
}
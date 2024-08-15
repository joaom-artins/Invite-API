using Invite.Commons;
using Invite.Commons.Notifications.Interfaces;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;

namespace Invite.Services.v1;

public class SystemService(
    AppSettings _appSettings,
    INotificationContext _notificationContext,
    IUnitOfWork _unitOfWork,
    IUserRepository _userRepository,
    IInvoiceService _invoiceService
) : ISystemService
{
    public async Task<bool> AutomatedInvoiceCreateasync()
    {
        var users = await _userRepository.FindByDueDayAsync(DateTime.Now.AddDays(_appSettings.Invoice.DaysBeforeCreate).Day);
        if (!users.Any())
        {
            return false;
        }

        foreach (var user in users)
        {
            try
            {
                _unitOfWork.BeginTransaction();
                await _invoiceService.CreateAsync(user.Id);
                if (_notificationContext.HasNotifications)
                {
                    continue;
                }
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
            }
        }

        return true;
    }
}

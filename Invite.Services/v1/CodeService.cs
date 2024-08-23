using Invite.Business.Interfaces.v1;
using Invite.Commons;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Models;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;

namespace Invite.Services.v1;

public class CodeService(
    IUnitOfWork _unitOfWork,
    INotificationContext _notificationContext,
    ICodeRepository _codeRepository,
    ICodeBusiness _codeBusiness
) : ICodeService
{
    public async Task<string> CreateAsync(Guid userId)
    {
        await _codeBusiness.ValidateForCreateAsync(userId);
        if (_notificationContext.HasNotifications)
        {
            return default!;
        }

        var record = new CodeModel
        {
            UserId = userId,
            Code = References.Generate(6),
        };
        await _codeRepository.AddAsync(record);
        await _unitOfWork.CommitAsync();

        return record.Code;
    }
}

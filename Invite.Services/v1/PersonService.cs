using Invite.Business.Interfaces.v1;
using Invite.Commons;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;

namespace Invite.Services.v1;

public class PersonService(
    INotificationContext _notificationContext,
    IUnitOfWork _unitOfWork,
    IPersonsRepository _personsRepository,
    IPersonBusiness _personBusiness
) : IPersonService
{
    public async Task<bool> CreateAsync(Guid responsibleId, PersonCreateRequest request)
    {
        await _personBusiness.ValidateForCreate(request);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        var record = new PersonModel
        {
            Name = request.Name,
            CPF = CleanString.OnlyNumber(request.CPF),
            ResponsibleId = responsibleId
        };
        await _personsRepository.AddAsync(record);
        await _unitOfWork.CommitAsync();

        return true;
    }
}

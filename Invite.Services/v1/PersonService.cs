using Invite.Business.Interfaces.v1;
using Invite.Commons;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Services.v1;

public class PersonService(
    INotificationContext _notificationContext,
    IUnitOfWork _unitOfWork,
    IPersonsRepository _personsRepository,
    IResponsibleRepository _responsibleRepository,
    IPersonBusiness _personBusiness
) : IPersonService
{
    public async Task<IEnumerable<PersonModel>> FindByResponsible(Guid responsibleId)
    {
        var records = await _personsRepository.GetByResponsible(responsibleId);

        return records;
    }

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

    public async Task<bool> AddToResponsibleAsync(Guid id, PersonCreateRequest request)
    {
        var record = await _responsibleRepository.GetByIdAsync(id);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Responsible.NotFound
            );
            return false;
        }

        _unitOfWork.BeginTransaction();

        record.PersonsInFamily++;
        _responsibleRepository.Update(record);
        await _unitOfWork.CommitAsync();

        await CreateAsync(record.Id, request);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        await _unitOfWork.CommitAsync(true);

        return true;
    }

    public async Task<bool> RemoveFromResponsibleAsync(Guid responsibleId, Guid id)
    {
        var record = await _personsRepository.GetByIdAndResponsible(id, responsibleId);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Person.NotFound
            );
            return false;
        }

        _personsRepository.Remove(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> RemoveAll(Guid responsibleId)
    {
        var records = await _personsRepository.GetByResponsible(responsibleId);
        if (!records.Any())
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Person.PersonsEmpty
            );
            return false;
        }

        _personsRepository.RemoveRange(records);
        await _unitOfWork.CommitAsync();

        return true;
    }
}

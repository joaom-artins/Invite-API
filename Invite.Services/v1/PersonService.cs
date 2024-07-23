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
    public async Task<IEnumerable<PersonModel>> FindByResponsible(Guid eventId, Guid inviteId, Guid responsibleId)
    {
        var records = await _personsRepository.GetByEventAndInviteAndResponsibleAsync(eventId, inviteId, responsibleId);

        return records;
    }

    public async Task<bool> CreateAsync(Guid eventId, Guid inviteId, Guid responsibleId, PersonCreateRequest request)
    {
        await _personBusiness.ValidateForCreateAsync(eventId, inviteId, responsibleId, request);
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

    public async Task<bool> AddToResponsibleAsync(Guid eventId, Guid inviteId, Guid responsibleId, PersonCreateRequest request)
    {
        var record = await _responsibleRepository.GetByIdAndEventAndInviteAsync(responsibleId, eventId, inviteId);
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

        await CreateAsync(eventId, inviteId, record.Id, request);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        await _unitOfWork.CommitAsync(true);

        return true;
    }

    public async Task<bool> RemoveFromResponsibleAsync(Guid eventId, Guid inviteId, Guid responsibleId, Guid id)
    {
        var responsbileRecord = await _responsibleRepository.GetByIdAndEventAndInviteAsync(responsibleId, eventId, inviteId);
        if (responsbileRecord is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Responsible.NotFound
            );
            return false;
        }

        var personrecord = await _personsRepository.GetByIdAndResponsible(id, eventId, inviteId, responsibleId);
        if (personrecord is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Person.NotFound
            );
            return false;
        }

        _unitOfWork.BeginTransaction();

        responsbileRecord.PersonsInFamily--;
        _responsibleRepository.Update(responsbileRecord);
        await _unitOfWork.CommitAsync();


        _personsRepository.Remove(personrecord);
        await _unitOfWork.CommitAsync();

        await _unitOfWork.CommitAsync(true);

        return true;
    }

    public async Task<bool> RemoveAll(Guid eventId, Guid inviteId, Guid responsibleId)
    {
        var responsbileRecord = await _responsibleRepository.GetByIdAndEventAndInviteAsync(responsibleId, eventId, inviteId);
        if (responsbileRecord is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Responsible.NotFound
            );
            return false;
        }

        var persons = await _personsRepository.GetByEventAndInviteAndResponsibleAsync(eventId, inviteId, responsibleId);
        if (!persons.Any())
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Person.PersonsEmpty
            );
            return false;
        }

        _unitOfWork.BeginTransaction();

        responsbileRecord.PersonsInFamily = 0;
        _responsibleRepository.Update(responsbileRecord);
        await _unitOfWork.CommitAsync();

        _personsRepository.RemoveRange(persons);
        await _unitOfWork.CommitAsync();

        await _unitOfWork.CommitAsync(true);

        return true;
    }
}

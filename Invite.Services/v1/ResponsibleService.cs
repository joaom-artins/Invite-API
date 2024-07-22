using AutoMapper;
using Invite.Business.Interfaces.v1;
using Invite.Commons;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Entities.Responses;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Services.v1;

public class ResponsibleService(
    IUnitOfWork _unitOfWork,
    IMapper _mapper,
    INotificationContext _notificationContext,
    IResponsibleRepository _responsibleRepository,
    IInviteRepository _inviteRepository,
    IResponsibleBusiness _responsibleBusiness,
    IPersonService _personService
) : IResponsibleService
{
    public async Task<IEnumerable<ResponsibleModel>> GetAll()
    {
        var records = await _responsibleRepository.GetAllAsync();

        return records;
    }

    public async Task<ResponsibleResponse> GetById(Guid id, Guid eventId, Guid inviteId)
    {
        var record = await _responsibleRepository.GetByIdAndEventAndInvite(id, eventId, inviteId);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Responsible.NotFound
            );
            return default!;
        }

        return _mapper.Map<ResponsibleResponse>(record);
    }

    public async Task<bool> CreateAsync(Guid eventId, Guid inviteId, ResponsibleCreateRequest request)
    {
        var inviteRecord = await _responsibleBusiness.ValidateForCreateAsync(eventId, inviteId, request);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        _unitOfWork.BeginTransaction();

        var record = new ResponsibleModel
        {
            Id = inviteRecord.FutureResponsibleId,
            Name = request.Name,
            PersonsInFamily = request.PersonInFamily,
            CPF = CleanString.OnlyNumber(request.CPF),
            InviteId = inviteRecord.Id
        };
        await _responsibleRepository.AddAsync(record);
        await _unitOfWork.CommitAsync();

        foreach (var person in request.Persons)
        {
            await _personService.CreateAsync(record.Id, person);
            if (_notificationContext.HasNotifications)
            {
                return false;
            }
        }

        inviteRecord.Acepted = true;
        _inviteRepository.Update(inviteRecord);
        await _unitOfWork.CommitAsync();

        await _unitOfWork.CommitAsync(true);

        return true;
    }

    public async Task<bool> UpdateAsync(Guid id, Guid eventId, Guid inviteId, ResponsibleUpdateRequest request)
    {
        var record = await _responsibleRepository.GetByIdAndEventAndInvite(id, eventId, inviteId);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Responsible.NotFound
            );
            return default!;
        }

        record.Name = request.Name;
        record.CPF = CleanString.OnlyNumber(request.CPF);
        _responsibleRepository.Update(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, Guid eventId, Guid inviteId)
    {
        var record = await _responsibleRepository.GetByIdAndEventAndInvite(id, eventId, inviteId);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Responsible.NotFound
            );
            return default!;
        }

        _responsibleRepository.Remove(record);
        await _unitOfWork.CommitAsync();

        return true;
    }
}

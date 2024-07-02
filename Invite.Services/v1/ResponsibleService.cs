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

public class ResponsibleService(
    IUnitOfWork _unitOfWork,
    INotificationContext _notificationContext,
    IResponsibleRepository _responsibleRepository,
    IResponsibleBusiness _responsibleBusiness
) : IResponsibleService
{
    public async Task<IEnumerable<ResponsibleModel>> GetAll()
    {
        var records = await _responsibleRepository.GetAllAsync();

        return records;
    }

    public async Task<ResponsibleModel> GetById(Guid id)
    {
        var record = await _responsibleRepository.GetByIdAsync(id);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Responsible.NotFound
            );
            return default!;
        }

        return record;
    }

    public async Task<bool> CreateAsync(ResponsibleCreateRequest request)
    {
        await _responsibleBusiness.ValidateForCreateAsync(request);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        var record = new ResponsibleModel
        {
            Name = request.Name,
            PersonsInFamily = request.PersonInFamily,
            CPF = CleanString.OnlyNumber(request.CPF),
        };
        await _responsibleRepository.AddAsync(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(Guid id, ResponsibleUpdateRequest request)
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

        record.Name = request.Name;
        record.CPF = request.CPF;
        record.PersonsInFamily = request.PersonInFamily;
        _responsibleRepository.Update(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var record = await _responsibleRepository.GetByIdAsync(id);
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

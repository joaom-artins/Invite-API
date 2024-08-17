using Invite.Business.Interfaces.v1;
using Invite.Commons.LoggedUsers.Interfaces;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Services.v1;

public class CerimonialistService(
    IUnitOfWork _unitOfWork,
    INotificationContext _notificationContext,
    ILoggedUser _loggedUser,
    ICerimonialistRepository _cerimonialistRepository,
    ICerimonialistBusiness _cerimonialistBusiness
) : ICerimonialistService
{
    public async Task<IEnumerable<CerimonialistModel>> GetAllAsync()
    {
        var records = await _cerimonialistRepository.GetAllAsync();

        return records;
    }

    public async Task<IEnumerable<CerimonialistModel>> SearchByNameAsync(string name)
    {
        var records = await _cerimonialistRepository.GetByNameAsync(name);

        return records;
    }

    public async Task<CerimonialistModel> GetByIdAsync(Guid id)
    {
        var record = await _cerimonialistRepository.GetByIdAsync(id);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Cerimonialist.NotFound
            );
            return default!;
        }

        return record;
    }

    public async Task<bool> CreateAsync(CerimonialistCreateRequest request)
    {
        await _cerimonialistBusiness.CheckExistsByUserAsync(_loggedUser.GetId());
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        var record = new CerimonialistModel
        {
            Name = request.Name,
            StartPrice = request.StartPrice,
            City = request.City,
            State = request.State,
            UserId = _loggedUser.GetId()
        };
        await _cerimonialistRepository.AddAsync(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(Guid id, CerimonialistUpdateRequest request)
    {
        var record = await _cerimonialistRepository.GetByIdAndUserAsync(id, _loggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Cerimonialist.NotFound
            );
            return false;
        }

        record.Name = request.Name;
        record.StartPrice = request.StartPrice;
        record.City = request.City;
        record.State = request.State;
        _cerimonialistRepository.Update(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> RemoveAsync(Guid id)
    {
        var record = await _cerimonialistRepository.GetByIdAndUserAsync(id, _loggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Cerimonialist.NotFound
            );
            return false;
        }

        _cerimonialistRepository.Remove(record);
        await _unitOfWork.CommitAsync();

        return true;
    }
}

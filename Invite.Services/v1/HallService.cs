using Invite.Business.Interfaces.v1;
using Invite.Commons;
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

public class HallService(
    INotificationContext _notificationContext,
    IUnitOfWork _unitOfWork,
    ILoggedUser _loggedUser,
    IHallRepository _hallRepository,
    IHallBusiness _hallBusiness
) : IHallService
{
    public async Task<IEnumerable<HallModel>> GetAllAsync()
    {
        var records = await _hallRepository.GetAllAsync();

        return records;
    }

    public async Task<HallModel> GetByIdAsync(Guid id)
    {
        var record = await _hallRepository.GetByIdAsync(id);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Hall.NotFound
            );
            return default!;
        }

        return record;
    }

    public async Task<bool> CreateAsync(HallCreateRequest request)
    {
        await _hallBusiness.ExistsByName(_loggedUser.GetId(), request.Name);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        var record = new HallModel
        {
            Name = request.Name,
            OwnerNumber = CleanString.OnlyNumber(request.OwnerNumber),
            CEP = CleanString.OnlyNumber(request.CEP),
            State = request.State,
            City = request.City,
            Street = request.Street,
            Number = request.Number,
            PriceInWeek = request.PriceInWeek,
            PriceInWeekend = request.PriceInWeekend,
            UserId = _loggedUser.GetId()
        };
        await _hallRepository.AddAsync(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(Guid id, HallUpdateRequest request)
    {
        await _hallBusiness.ExistsByName(_loggedUser.GetId(), request.Name);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        var record = await _hallRepository.GetByIdAndUserAsync(id, _loggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Hall.NotFound
            );
            return false;
        }

        record.Name = request.Name;
        record.OwnerNumber = CleanString.OnlyNumber(request.OwnerNumber);
        record.CEP = CleanString.OnlyNumber(request.CEP);
        record.State = request.State;
        record.City = request.City;
        record.Street = request.Street;
        record.Number = request.Number;
        record.PriceInWeek = request.PriceInWeek;
        record.PriceInWeekend = request.PriceInWeekend;
        _hallRepository.Update(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var record = await _hallRepository.GetByIdAndUserAsync(id, _loggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Hall.NotFound
            );
            return false;
        }
        _hallRepository.Remove(record);
        await _unitOfWork.CommitAsync();

        return true;
    }
}
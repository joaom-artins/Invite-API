using Invite.Business.Interfaces.v1;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;
using Microsoft.AspNetCore.Http;

namespace Invite.Services.v1;

public class PlanService(
    INotificationContext _notificationContext,
    IUnitOfWork _unitOfWork,
    IPlanBusiness _planBusiness,
    IPlanRepository _planRepository
) : IPlanService
{
    public async Task<IEnumerable<PlanModel>> GetAllAsync()
    {
        var records = await _planRepository.GetAllAsync();

        return records;
    }

    public async Task<IEnumerable<PlanModel>> GetByNameAsync(string name)
    {
        var records = await _planRepository.GetByNameAsync(name);

        return records;
    }

    public async Task<bool> CreateAsync(PlanCreateRequest request)
    {
        await _planBusiness.ExistsNameAsync(request.Name);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        var record = new PlanModel
        {
            Name = request.Name,
            Price = request.Price,
            Type = request.Type,
        };
        await _planRepository.AddAsync(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(Guid id, PlanUpdateRequest request)
    {
        await _planBusiness.ExistsNameAsync(request.Name);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        var record = await _planRepository.GetByIdAsync(id);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Plan.NotFound
            );
            return false;
        }

        record.Name = request.Name;
        record.Price = request.Price;
        _planRepository.Update(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> DeleteAync(Guid id)
    {
        var record = await _planRepository.GetByIdAsync(id);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Plan.NotFound
            );
            return false;
        }

        _planRepository.Remove(record);
        await _unitOfWork.CommitAsync();

        return true;
    }
}

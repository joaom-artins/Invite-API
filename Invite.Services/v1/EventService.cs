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

public class EventService(
    INotificationContext _notificationContext,
    IUnitOfWork _unitOfWork,
    ILoggedUser _loggedUser,
    IEventBusiness _eventBusiness,
    IInvoiceService _invoiceService,
    IEventRepository _eventRepository,
    IHallRepository _hallRepository
) : IEventService
{
    public async Task<IEnumerable<EventModel>> GetAllAsync()
    {
        var records = await _eventRepository.FindByUserAsync(_loggedUser.GetId());

        return records;
    }

    public async Task<EventModel> GetByIdAsync(Guid id)
    {
        var record = await _eventRepository.GetByIdAndUserAsync(id, _loggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Event.NotFound
            );
            return default!;
        }

        return record;
    }

    public async Task<bool> CreateAsync(Guid planId, EventCreateRequest request)
    {
        await _eventBusiness.ValidatePlanAsync(planId, _loggedUser.GetId(), request.Guests);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        _unitOfWork.BeginTransaction();

        if (request.UseHallRegistred && request.HallId is not null)
        {
            request.City = null;
            request.CEP = null;
            request.State = null;
            request.Street = null;
            request.Number = null;

            var hallRecord = await _hallRepository.GetByIdAsync(request.HallId.Value);
            if (hallRecord is null)
            {
                _notificationContext.SetDetails(
                    statusCode: StatusCodes.Status404NotFound,
                    title: NotificationTitle.NotFound,
                    detail: NotificationMessage.Hall.NotFound
                );
                return false;
            }
        }
        else
        {
            request.HallId = null;
        }

        var record = new EventModel
        {
            Name = request.Name,
            Type = request.Type,
            PlanId = planId,
            UserId = _loggedUser.GetId(),
            Guests = request.Guests,
            Date = request.Date,
            City = request.City is null ? null : request.City,
            State = request.City is null ? null : request.State,
            Street = request.City is null ? null : request.Street,
            Number = request.City is null ? null : request.Number,
            CEP = request.City is null ? null : CleanString.OnlyNumber(request.CEP!),
            HallId = request.City is null ? null : request.HallId,
        };
        await _eventRepository.AddAsync(record);
        await _unitOfWork.CommitAsync();

        await _invoiceService.CreateAsync(eventModel: record);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        await _unitOfWork.CommitAsync(true);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var record = await _eventRepository.GetByIdAndUserAsync(id, _loggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Event.NotFound
            );
            return false;
        }
        _eventRepository.Remove(record);
        await _unitOfWork.CommitAsync();

        return true;
    }
}

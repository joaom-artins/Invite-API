using AutoMapper;
using Invite.Business.Interfaces.v1;
using Invite.Commons;
using Invite.Commons.LoggedUsers.Interfaces;
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

public class BuffetService(
    INotificationContext _notificationContext,
    ILoggedUser _loggedUser,
    IMapper _mapper,
    IUnitOfWork _unitOfWork,
    IBuffetBusiness _buffetBusiness,
    IInvoiceService _invoiceService,
    IBuffetRepository _buffetRepository,
    ICommentRepository _commentRepository
) : IBuffetService
{
    public async Task<IEnumerable<BuffetResponse>> GetAllAsync()
    {
        var records = await _buffetRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<BuffetResponse>>(records);
    }

    public async Task<BuffetResponse> GetByIdAsync(Guid id)
    {
        var record = await _buffetRepository.GetByIdAsync(id);
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Buffet.NotFound
           );
            return default!;
        }

        return _mapper.Map<BuffetResponse>(record);
    }

    public async Task<bool> CreateAsync(BuffetCreateRequest request)
    {
        await _buffetBusiness.ValidateForCreateAndUpdateAsync(request.Name, request.CNPJ, request.PhoneNumber);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        _unitOfWork.BeginTransaction();

        var record = new BuffetModel
        {
            UserId = _loggedUser.GetId(),
            Name = request.Name,
            PhoneNumber = CleanString.OnlyNumber(request.PhoneNumber),
            CNPJ = CleanString.OnlyNumber(request.CNPJ),
            City = request.City,
            State = request.State,
            ServeInRadius = request.ServeInRadius
        };
        await _buffetRepository.AddAsync(record);
        await _unitOfWork.CommitAsync();

        await _invoiceService.CreateAsync(buffet: record);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        await _unitOfWork.CommitAsync(true);

        return true;
    }

    public async Task<bool> UpdateRateAsync(BuffetModel buffet)
    {
        var comments = await _commentRepository.FindByBuffetAsync(buffet.Id);
        var sum = 0;
        foreach (var comment in comments)
        {
            sum += comment.Stars;
        }

        buffet.Rate = sum / comments.Count();
        _buffetRepository.Update(buffet);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> UpdateAsync(Guid id, BuffetUpdateRequest request)
    {
        var record = await _buffetRepository.GetByIdAndUserAsync(id, _loggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Buffet.NotFound
           );
            return false;
        }

        await _buffetBusiness.ValidateForCreateAndUpdateAsync(request.Name, request.CNPJ, request.PhoneNumber);
        if (_notificationContext.HasNotifications)
        {
            return false;
        }

        record.Name = request.Name;
        record.PhoneNumber = CleanString.OnlyNumber(request.PhoneNumber);
        record.CNPJ = CleanString.OnlyNumber(request.CNPJ);
        record.City = request.City;
        record.State = request.State;
        record.ServeInRadius = request.ServeInRadius;
        _buffetRepository.Update(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var record = await _buffetRepository.GetByIdAndUserAsync(id, _loggedUser.GetId());
        if (record is null)
        {
            _notificationContext.SetDetails(
                statusCode: StatusCodes.Status404NotFound,
                title: NotificationTitle.NotFound,
                detail: NotificationMessage.Buffet.NotFound
           );
            return false;
        }

        _buffetRepository.Remove(record);
        await _unitOfWork.CommitAsync();

        return true;
    }
}

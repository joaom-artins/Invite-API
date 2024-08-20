using AutoMapper;
using Invite.Business.Interfaces.v1;
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

public class CerimonialistService(
    IUnitOfWork _unitOfWork,
    INotificationContext _notificationContext,
    IMapper _mapper,
    ILoggedUser _loggedUser,
    ICerimonialistRepository _cerimonialistRepository,
    ICommentRepository _commentRepository,
    ICerimonialistBusiness _cerimonialistBusiness,
    IInvoiceService _invoiceService
) : ICerimonialistService
{
    public async Task<IEnumerable<CerimonialistReponse>> GetAllAsync()
    {
        var records = await _cerimonialistRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<CerimonialistReponse>>(records);
    }

    public async Task<IEnumerable<CerimonialistReponse>> SearchByNameAsync(string name)
    {
        var records = await _cerimonialistRepository.GetByNameAsync(name);

        return _mapper.Map<IEnumerable<CerimonialistReponse>>(records);;
    }

    public async Task<CerimonialistReponse> GetByIdAsync(Guid id)
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

        return _mapper.Map<CerimonialistReponse>(record);
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

        await _invoiceService.CreateAsync(_loggedUser.GetId(), false, cerimonialist: record);

        return true;
    }

    public async Task<bool> UpdateRateAsync(CerimonialistModel cerimonialist)
    {
        var comments = await _commentRepository.FindByCerimonialistAsync(cerimonialist.Id);
        var avg = comments.Average(comment => comment.Stars);

        cerimonialist.Rate = avg;
        _cerimonialistRepository.Update(cerimonialist);
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

        var comments = await _commentRepository.FindByCerimonialistAsync(id);
        if(comments.Any())
        {
            _commentRepository.RemoveRange(comments);
            await _unitOfWork.CommitAsync();
        }

        _cerimonialistRepository.Remove(record);
        await _unitOfWork.CommitAsync();

        return true;
    }
}

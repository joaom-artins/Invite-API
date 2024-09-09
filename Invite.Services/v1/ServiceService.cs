using Invite.Entities.Models;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;

namespace Invite.Services.v1;

public class ServiceService(
    IUnitOfWork _unitOfWork,
    IServiceRepository _serviceRepository
) : IServiceService
{
    public async Task<bool> CreateAsync(Guid userId)
    {
        var record = await _serviceRepository.GetByUserAsync(userId);
        if (record is null)
        {
            var newService = new ServiceModel
            {
                UserId = userId,
            };
            await _serviceRepository.AddAsync(newService);
            await _unitOfWork.CommitAsync();
        }

        return true;
    }

    public async Task<bool> UpdateNextDueDateAsync(ServiceModel service, int userDueDay)
    {
        var nextDueDate = service.NextDueDate;
        if (nextDueDate!.Value.Day > 28)
        {
            nextDueDate = nextDueDate.Value.AddMonths(2);
        }
        else
        {
            nextDueDate = nextDueDate.Value.AddMonths(1);
        }

        nextDueDate = new DateOnly(nextDueDate!.Value.Year, nextDueDate.Value.Month, Convert.ToInt16(userDueDay));

        service.NextDueDate = nextDueDate;
        _serviceRepository.Update(service);
        await _unitOfWork.CommitAsync();

        return true;
    }
}

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
}

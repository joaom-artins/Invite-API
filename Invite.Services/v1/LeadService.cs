using Invite.Entities.Models;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Services.Interfaces.v1;

namespace Invite.Services.v1;

public class LeadService(
    IUnitOfWork _unitOfWork,
    ILeadRepository _leadRepository
) : ILeadService
{
    public async Task<bool> CreateAsync(LeadCreateRequest request)
    {
        var record = new LeadModel
        {
            FullName= request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
        };
        await _leadRepository.AddAsync(record);
        await _unitOfWork.CommitAsync();

        return true;
    }
}

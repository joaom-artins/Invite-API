using Invite.Commons;
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
    public async Task<IEnumerable<LeadModel>> GetAllAsync()
    {
        var records = await _leadRepository.GetAllAsync();

        return records;
    }

    public async Task<bool> CreateAsync(LeadCreateRequest request)
    {
        var record = new LeadModel
        {
            FullName = request.FullName,
            Email = request.Email,
            PhoneNumber = CleanString.OnlyNumber(request.PhoneNumber),
        };
        await _leadRepository.AddAsync(record);
        await _unitOfWork.CommitAsync();

        return true;
    }

    public async Task<bool> RemoveRange(string email)
    {
        var records = await _leadRepository.GetByEmailAsync(email);
        if (records.Any())
        {
            _leadRepository.RemoveRange(records);
            await _unitOfWork.CommitAsync();
        }

        return true;
    }
}

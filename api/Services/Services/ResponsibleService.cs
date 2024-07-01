using api.Business.Interfaces;
using api.Entities.Models;
using api.Entities.Request;
using api.Persistence.Repositories.Interfaces;
using api.Persistence.UnitOfWorks.Interfaces;
using api.Services.Interfaces;

namespace api.Services.Services;

public class ResponsibleService(
    IUnitOfWork _unitOfWork,
    IResponsibleRepository _responsibleRepository,
    IResponsibleBusiness _responsibleBusiness
) : IResponsibleService
{
    public async Task<IEnumerable<ResponsibleModel>> GetAll()
    {
        var records = await _responsibleRepository.GetAllAsync();

        return records;
    }

    public async Task<ResponsibleModel> GetById(Guid id)
    {
        var record = await _responsibleRepository.GetByIdAsync(id);
        if (record is null)
        {
            return default!;
        }

        return record;
    }
    public async Task<bool> CreateAsync(ResponsibleCreateRequest request)
    {
        var test = await _responsibleBusiness.ValidateForCreateAsync(request);
        if (test is false)
        {
            return false;
        }
        var record = new ResponsibleModel
        {
            Name = request.Name,
            PersonsInFamily = request.PersonsInFamily,
            CPF = request.CPF,
        };
        await _responsibleRepository.AddAsync(record);
        await _unitOfWork.CommitAsync();

        return true;
    }
}

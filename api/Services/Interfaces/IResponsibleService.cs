using api.Entities.Models;
using api.Entities.Request;

namespace api.Services.Interfaces;

public interface IResponsibleService
{
    Task<IEnumerable<ResponsibleModel>> GetAll();
    Task<ResponsibleModel> GetById(Guid id);
    Task<bool> CreateAsync(ResponsibleCreateRequest request);
}

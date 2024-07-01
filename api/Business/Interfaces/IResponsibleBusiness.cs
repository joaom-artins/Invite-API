using api.Entities.Request;

namespace api.Business.Interfaces;

public interface IResponsibleBusiness
{
    Task<bool> ValidateForCreateAsync(ResponsibleCreateRequest request);
}

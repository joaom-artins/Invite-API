using api.Business.Interfaces;
using api.Common;
using api.Entities.Request;
using api.Persistence.Repositories.Interfaces;

namespace api.Business.Business;

public class ResponsibleBusiness(
    IResponsibleRepository _responsibleRepository
) : IResponsibleBusiness
{
    public async Task<bool> ValidateForCreateAsync(ResponsibleCreateRequest request)
    {
        var cpfIsValid = ValidateCPF.IsValidCpf(request.CPF);
        if (!cpfIsValid)
        {
            return false;
        }

        cpfIsValid = ValidateCPF.IsCpfFormatValid(request.CPF);
        if (!cpfIsValid)
        {
            return false;
        }

        var exists = await _responsibleRepository.ExistsByCPF(request.CPF);
        if (exists)
        {
            return false;
        }

        return true;
    }
}

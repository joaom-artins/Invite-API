using Invite.Business.Interfaces.v1;
using Invite.Commons;
using Invite.Entities.Requests;
using Invite.Persistence.Repositories.Interfaces.v1;

namespace Invite.Business.v1;

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

        var exists = await _responsibleRepository.ExistsByCpf(request.CPF);
        if (exists)
        {
            return false;
        }

        return true;
    }
}

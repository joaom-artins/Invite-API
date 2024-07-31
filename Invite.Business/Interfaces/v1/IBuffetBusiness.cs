using Invite.Entities.Requests;

namespace Invite.Business.Interfaces.v1;

public interface IBuffetBusiness
{
    Task<bool> ValidateForCreateAndUpdateAsync(string name, string cnpj, string phoneNumber);
}
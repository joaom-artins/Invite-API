using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface IBuffetRepository : IGenericRepository<BuffetModel>
{
    Task<bool> ExistsByNameAndCNPJAndPhoneNumberAsync(string name, string cnpj, string phoneNumber);
}

using Invite.Entities.Models;

namespace Invite.Persistence.Repositories.Interfaces.v1;

public interface ILeadRepository : IGenericRepository<LeadModel>
{
    Task<IEnumerable<LeadModel>> GetByEmailAsync(string email);
}

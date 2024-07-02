using Invite.Entities.Models;
using Invite.Persistence.Context;
using Invite.Persistence.Repositories.Interfaces.v1;

namespace Invite.Persistence.Repositories.v1;

public class PersonRepository(
    AppDbContext context
) : GenericRepository<PersonModel>(context),
    IPersonsRepository
{
}

using api.Entities.Models;
using api.Persistence.Context;
using api.Persistence.Repositories.Interfaces;

namespace api.Persistence.Repositories.Repository;

public class PersonRepository(
    AppDbContext context
) : GenericRepository<PersonModel>(context),
    IPersonsRepository
{
}

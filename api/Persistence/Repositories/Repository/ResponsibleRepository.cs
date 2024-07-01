using api.Entities.Models;
using api.Persistence.Context;
using api.Persistence.Repositories.Interfaces;

namespace api.Persistence.Repositories.Repository;

public class ResponsibleRepository(
    AppDbContext context
) : GenericRepository<ResponsibleModel>(context),
    IResponsibleRepository
{
}

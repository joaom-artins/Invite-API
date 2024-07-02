using Invite.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<ResponsibleModel> Responsibles { get; set; } = default!;
    public DbSet<PersonModel> Persons { get; set; } = default!;
}

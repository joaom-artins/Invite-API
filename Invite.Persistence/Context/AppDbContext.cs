using Invite.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Invite.Persistence.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<UserModel, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<BuffetModel> Buffets { get; set; } = default!;
    public DbSet<CommentModel> Comments { get; set; } = default!;
    public DbSet<HallModel> Halls { get; set; } = default!;
    public DbSet<EventModel> Events { get; set; } = default!;
    public DbSet<InviteModel> Invites { get; set; } = default!;
    public DbSet<InvoiceModel> Invoices { get; set; } = default!;
    public DbSet<InvoiceItemizedModel> InvoiceItemizeds { get; set; } = default!;
    public override DbSet<UserModel> Users { get; set; } = default!;
    public DbSet<ResponsibleModel> Responsibles { get; set; } = default!;
    public DbSet<PlanModel> Plans { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserModel>().ToTable("Users");
        modelBuilder.Entity<IdentityRole<Guid>>().ToTable("Roles");
        modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("RolesClaim");
        modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UsersRoles");
        modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UsersTokens");
        modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UsersLogins");
        modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("UsersClaims");

        modelBuilder.Entity<IdentityUserRole<Guid>>().HasKey(p => new { p.UserId, p.RoleId });
    }
}

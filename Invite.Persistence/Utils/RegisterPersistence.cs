using Invite.Persistence.Context;
using Invite.Persistence.Repositories.v1;
using Invite.Persistence.UnitOfWorks.Interfaces;
using Invite.Persistence.UnitOfWorks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Invite.Persistence.Repositories.Interfaces.v1;

namespace Invite.Persistence.Utils;

public class RegisterPersistence
{
    public static void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddScoped<IEventRepository, EventRepository>();
        builder.Services.AddScoped<IInviteRepository, InviteRepository>();
        builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
        builder.Services.AddScoped<IInvoiceItemizedRepository, InvoiceItemizedRepository>();
        builder.Services.AddScoped<IHallRepository, HallRepository>();
        builder.Services.AddScoped<IPlanRepository, PlanRepository>();
        builder.Services.AddScoped<IResponsibleRepository, ResponsibleRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
    }
}
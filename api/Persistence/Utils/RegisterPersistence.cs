using api.Persistence.Context;
using api.Persistence.Repositories.Interfaces;
using api.Persistence.Repositories.Repository;
using api.Persistence.UnitOfWorks;
using api.Persistence.UnitOfWorks.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Persistence.Utils;

public class RegisterPersistence
{
    public static void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        builder.Services.AddScoped<IPersonsRepository, PersonRepository>();
        builder.Services.AddScoped<IResponsibleRepository, ResponsibleRepository>();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
    }
}

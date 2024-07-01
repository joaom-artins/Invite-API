using api.Persistence.Context;
using api.Persistence.Repositories.Interfaces;
using api.Persistence.Repositories.Repository;
using api.Persistence.UnitOfWork;
using api.Persistence.UnitOfWork.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Persistence.Utils;

public class RegisterPersistence
{
    public static void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

        builder.Services.AddScoped<IPersonsRepository, PersonRepository>();
        builder.Services.AddScoped<IResponsibleRepository, ResponsibleRepository>();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        );
    }
}

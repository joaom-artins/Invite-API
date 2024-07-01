using api.Services.Interfaces;
using api.Services.Services;

namespace api.Services.Utils;

public class RegisterService
{
    public static void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IResponsibleService, ResponsibleService>();
    }
}

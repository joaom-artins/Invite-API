using api.Business.Business;
using api.Business.Interfaces;

namespace api.Business.Utils;

public class RegisterBusiness
{
    public static void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IResponsibleBusiness, ResponsibleBusiness>();
    }
}

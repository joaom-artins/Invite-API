using Invite.Business.Interfaces.v1;
using Invite.Business.v1;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Invite.Business.Utils;

public class RegisterBusiness
{
    public static void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IResponsibleBusiness, ResponsibleBusiness>();
    }
}

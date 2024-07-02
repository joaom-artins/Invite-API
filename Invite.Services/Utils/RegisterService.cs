using Invite.Services.Interfaces.v1;
using Invite.Services.v1;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Invite.Services.Utils;

public class RegisterService
{
    public static void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IResponsibleService, ResponsibleService>();
    }
}

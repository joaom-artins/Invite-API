using Invite.Commons.LoggedUsers;
using Invite.Commons.LoggedUsers.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Invite.Commons.Utils;

public class RegisterCommons
{
    public static void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<ILoggedUser, LoggedUser>();
    }
}

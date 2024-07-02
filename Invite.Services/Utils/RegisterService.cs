using System.Text.Json;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Services.Interfaces.v1;
using Invite.Services.v1;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Invite.Services.Utils;

public class RegisterService
{
    public static void Register(WebApplicationBuilder builder)
    {
        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        builder.Services.AddSingleton(jsonSerializerOptions);
        builder.Services.AddScoped<INotificationContext, NotificationContext>();
        builder.Services.AddScoped<IResponsibleService, ResponsibleService>();
        builder.Services.AddScoped<IPersonService, PersonService>();
    }
}

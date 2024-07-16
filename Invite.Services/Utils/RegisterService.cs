using System.Text.Json;
using Invite.Commons;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Invite.Services.Interfaces.v1;
using Invite.Services.v1;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

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
        builder.Services.AddScoped<ILoginService, LoginService>();
        builder.Services.AddScoped<INotificationContext, NotificationContext>();
        builder.Services.AddScoped<IResponsibleService, ResponsibleService>();
        builder.Services.AddScoped<IPersonService, PersonService>();
        builder.Services.AddScoped<IPlanService, PlanService>();
        builder.Services.AddScoped<IUserService, UserService>();

        var appSettings = builder.Configuration.GetSection("AppSettings");
        builder.Services.Configure<AppSettings>(appSettings);
        builder.Services.AddSingleton(serviceProvider => serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value);
    }
}

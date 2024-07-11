using Invite.Business.Interfaces.v1;
using Invite.Business.v1;
using Invite.Commons.Notifications;
using Invite.Commons.Notifications.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Invite.Business.Utils;

public class RegisterBusiness
{
    public static void Register(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<INotificationContext, NotificationContext>();
        builder.Services.AddScoped<IResponsibleBusiness, ResponsibleBusiness>();
        builder.Services.AddScoped<IPersonBusiness, PersonBusiness>();
        builder.Services.AddScoped<IUserBusiness, UserBusiness>();
    }
}

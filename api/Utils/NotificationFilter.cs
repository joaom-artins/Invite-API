using System.Diagnostics;
using System.Text.Json;
using Invite.Commons.Notifications.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace api.Utils;

public class NotificationFilter(INotificationContext notificationContext, JsonSerializerOptions jsonSerializerOptions) : IAsyncResultFilter
{
    private readonly INotificationContext _notificationContext = notificationContext;
    private readonly JsonSerializerOptions _jsonSerializerOptions = jsonSerializerOptions;

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_notificationContext.HasNotifications)
        {
            var code = _notificationContext.StatusCode;
            string type = code switch
            {
                400 => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                401 => "https://tools.ietf.org/html/rfc7235#section-3.1",
                403 => "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                404 => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                409 => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                _ => "https://tools.ietf.org/html/rfc7231",
            };

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = code;

            ProblemDetails result;
            if (_notificationContext.IsEmptyNotificationList)
            {
                result = new ProblemDetails
                {
                    Type = type,
                    Status = _notificationContext.StatusCode,
                    Title = _notificationContext.Title,
                    Detail = _notificationContext.Detail,
                    Instance = context.HttpContext.Request.Path,
                    Extensions = {
                        { "traceId", Activity.Current?.Id }
                    }
                };
            }
            else
            {
                result = new ProblemDetails
                {
                    Type = type,
                    Status = _notificationContext.StatusCode,
                    Title = _notificationContext.Title,
                    Detail = _notificationContext.Detail,
                    Instance = context.HttpContext.Request.Path,
                    Extensions = {
                        { "traceId", Activity.Current?.Id },
                        { "errors", _notificationContext.Notifications }
                    }
                };
            }
            await context.HttpContext.Response.WriteAsync(JsonSerializer.Serialize(result, _jsonSerializerOptions));

            return;
        }

        await next();
    }
}

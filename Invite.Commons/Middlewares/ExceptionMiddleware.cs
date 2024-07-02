using System.Diagnostics;
using System.Text.Json;
using Invite.Commons.Notifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Invite.Commons.Middlewares;

public class ExceptionMiddleware(RequestDelegate _next, JsonSerializerOptions _jsonSerializerOptions)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var code = StatusCodes.Status500InternalServerError;

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = code;

        return context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Status = code,
            Instance = context.Request.Path,
            Title = NotificationTitle.InternalServerError,
            Detail = NotificationMessage.Common.UnexpectedError,
            Extensions = { { "traceId", Activity.Current?.Id } }
        }, _jsonSerializerOptions));
    }
}

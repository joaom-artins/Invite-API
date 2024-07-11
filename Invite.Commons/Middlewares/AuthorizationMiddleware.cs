using Microsoft.AspNetCore.Http;

namespace Invite.Commons.Middlewares;

public class AuthorizationMiddleware(RequestDelegate _next)
{
    public async Task Invoke(HttpContext context)
    {
        var tokenName = "AUTH_TOKEN";

        if (context.Request.Cookies.ContainsKey(tokenName) && !context.Request.Headers.ContainsKey("Authorization"))
        {
            context.Request.Headers.Append("Authorization", $"Bearer {context.Request.Cookies[tokenName]}");
        }

        await _next(context);
    }
}

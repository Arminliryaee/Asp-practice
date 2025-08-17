namespace Middleware_Practice.Middlewares;

public class CustomHeaderMiddleware
{
    private readonly RequestDelegate _next;

    public CustomHeaderMiddleware(RequestDelegate _next)
    {
        this._next = _next;
    }
    public async Task InvokeAsync(HttpContext ctx)
    {
        ctx.Response.Headers.Append("X-App-Version", "1.0.0");
        await _next(ctx);
    }
}

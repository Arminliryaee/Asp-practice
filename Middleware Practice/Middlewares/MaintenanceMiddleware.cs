namespace Middleware_Practice.Middlewares;

public class MaintenanceMiddleware
{
    private readonly RequestDelegate _next;

    public MaintenanceMiddleware(RequestDelegate _next)
    {
        this._next = _next;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        string Path = ctx.Request.Path.Value.ToLower();
        if (Path.Contains("maintenance") || Path.Contains("home"))
        {
            ctx.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            await ctx.Response.WriteAsync("<h1>Site is under maintenance. Please check back later.</h1>");
            return;
        }
        else
            await _next(ctx);
    }
}

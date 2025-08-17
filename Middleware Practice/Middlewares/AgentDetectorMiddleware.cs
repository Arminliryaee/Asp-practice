namespace Middleware_Practice.Middlewares;

public class AgentDetectorMiddleware
{
    private readonly RequestDelegate _next;
    public AgentDetectorMiddleware(RequestDelegate _next)
    {
        this._next = _next;
    }
    public async Task InvokeAsync(HttpContext ctx)
    {
        string RequestHeader = ctx.Request.Headers["User-Agent"].ToString().ToLower();
        if (RequestHeader.Contains("Mobile") || RequestHeader.Contains("Android") || RequestHeader.Contains("iPhone"))
        {
            await _next(ctx);
            await ctx.Response.WriteAsync("Mobile User ");
        }
        else
        {
            await _next(ctx);
            await ctx.Response.WriteAsync("Windows/Mac User ");
        }
    }
}

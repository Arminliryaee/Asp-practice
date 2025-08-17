using Middleware_Practice.Middlewares;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseMiddleware<MaintenanceMiddleware>();

app.Use(async (ctx, next) =>
{
    ctx.Response.ContentType = "text/html; charset=utf-8";
    string PathValue = ctx.Request.Path.Value;
    if (PathValue.ToLower().Contains("debug"))
    {
        await next(ctx);
        await ctx.Response.WriteAsync("<div style='background-color: lightblue; border: 1px solid blue; padding: 10px;'>Debug mode is active.</div>\n");
    }
    else
        await next(ctx);
});

app.Use(async (ctx, next) =>
{
    string? PathValue = ctx.Request.Path.Value;
    if (PathValue is not null && PathValue.ToLower().StartsWith("/admin/"))
    {
        await next(ctx);
        await ctx.Response.WriteAsync("<div style='background-color: yellow; text-align: center; padding: 5px;'>Attention: You are in the admin section.</div>\n");
    }
    else
        await next(ctx);
});

app.Use(async (ctx, next) =>
{
    DateTime DateNow = DateTime.Now;
    await next(ctx);
    await ctx.Response.WriteAsync(DateNow.Hour switch
    {
        < 12 => "Good morning!\n",
        < 18 => "Good afternoon!\n",
        _ => "Good evening!\n"
    });
});

app.UseMiddleware<AgentDetectorMiddleware>();
app.UseMiddleware<CustomHeaderMiddleware>();


app.MapGet("/Home", () => "Nothing to show yet.\n");


app.Run();
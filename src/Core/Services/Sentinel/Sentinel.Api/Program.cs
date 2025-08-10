using Serilog;
using Sentinel.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, cfg) =>
{
    cfg.ReadFrom.Configuration(ctx.Configuration)
       .WriteTo.Console()
       .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day);
});

builder.Services.AddSentinelServices(builder.Configuration);

var app = builder.Build();

app.UseSentinel();

await app.SeedAsync();

app.Run();

using FluentValidation.AspNetCore;
using Sentinel.Api.Data.Seeds;
using Sentinel.Api.Extensions;
using Sentinel.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddAppLogging(builder.Configuration);
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.AddAppAuthentication(builder.Configuration);

builder.Services.AddServices();
builder.Services.AddAppRateLimiter();

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAppSwagger();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseAppLogging();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseCorrelationId();
app.UseAppRateLimiter();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.UseAppSwagger();
app.MapControllers();
app.MapHealthChecks("/health");

await app.SeedAsync();
await app.RunAsync();

using FluentValidation.AspNetCore;
using Sentinel.Api.Extensions;
using Sentinel.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddAppLogging(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddIdentity();
builder.Services.AddAppAuthentication(builder.Configuration);

builder.Services.AddServices();
builder.Services.AddAppRateLimiter(builder.Configuration);

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAppSwagger();
builder.Services.AddHealthChecks();

var app = builder.Build();

app.UseAppLogging();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseFrameAncestors();
app.UseExceptionHandler("/error");
app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseCorrelationId();
app.UseRouting();
app.UseAppRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();

app.UseAppSwagger();
app.MapControllers();
app.MapHealthChecks("/health");

await app.RunAsync();

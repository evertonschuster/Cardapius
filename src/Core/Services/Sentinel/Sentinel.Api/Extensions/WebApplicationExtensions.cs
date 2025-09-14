using Sentinel.Api.Middleware;

namespace Sentinel.Api.Extensions;

public static class WebApplicationExtensions
{
    public static WebApplication UseSentinel(this WebApplication app)
    {
        app.UseAppLogging();
        app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
        app.UseFrameAncestors();
        app.UseExceptionHandling();
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

        return app;
    }


}

using System.Diagnostics;
using System.Net;

namespace Sentinel.Api.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var traceId = Activity.Current?.Id ?? context.TraceIdentifier;
            _logger.LogError(ex, "Unhandled exception with trace id {TraceId}", traceId);

            var acceptsHtml = context.Request.Headers.TryGetValue("Accept", out var accept) &&
                               accept.Any(a => a.Contains("text/html", StringComparison.OrdinalIgnoreCase));

            if (acceptsHtml)
            {
                context.Response.Redirect("/error");
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new
                {
                    message = "Ocorreu um erro inesperado. Tente novamente mais tarde.",
                    traceId
                });
            }
        }
    }
}

public static class ExceptionHandlingExtensions
{
    public static IApplicationBuilder UseExceptionHandling(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionHandlingMiddleware>();
}


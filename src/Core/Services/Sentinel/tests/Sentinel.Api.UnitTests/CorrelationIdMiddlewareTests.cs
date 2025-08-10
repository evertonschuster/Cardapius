using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Sentinel.Api.Middleware;

public class CorrelationIdMiddlewareTests
{
    [Fact]
    public async Task AddsCorrelationIdWhenMissing()
    {
        var context = new DefaultHttpContext();
        RequestDelegate next = _ => Task.CompletedTask;
        var middleware = new CorrelationIdMiddleware(next);

        await middleware.InvokeAsync(context);

        context.Response.Headers.ContainsKey("X-Correlation-Id").Should().BeTrue();
    }
}

using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace BuildingBlock.Observability.ElasticStack.Middleware
{
    public class RequestSerilLogMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestSerilLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            using var property = LogContext.PushProperty("UserName", context?.User?.Identity?.Name ?? "unknown");
            return _next.Invoke(context!);
        }
    }
}

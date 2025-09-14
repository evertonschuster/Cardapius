using Sentinel.Api.Middleware;

namespace Sentinel.Api.Extensions
{
    public static class FrameAncestorsExtensions
    {
        public static IApplicationBuilder UseFrameAncestors(this IApplicationBuilder app)
        {
            return app.UseMiddleware<FrameAncestorsMiddleware>();
        }
    }
}

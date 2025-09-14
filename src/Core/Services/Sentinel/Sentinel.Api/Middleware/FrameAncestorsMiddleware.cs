namespace Sentinel.Api.Middleware
{
    public class FrameAncestorsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _policyValue;

        public FrameAncestorsMiddleware(RequestDelegate next)
        {
            _next = next;
            _policyValue = $"frame-ancestors http://localhost:3000";
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Remove("X-Frame-Options");
                context.Response.Headers["Content-Security-Policy"] = _policyValue;
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}

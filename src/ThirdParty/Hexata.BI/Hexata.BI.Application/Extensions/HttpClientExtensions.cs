using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using Polly.Fallback;
using Polly.RateLimiting;
using System.Net;
using System.Threading.RateLimiting;
namespace Hexata.BI.Application.Extensions
{
    internal static class HttpClientExtensions
    {
        public static IHttpClientBuilder AddHttpClientPolly(this IServiceCollection services)
        {
            var clientBuilder = services.AddHttpClient(string.Empty);
            clientBuilder.AddResilienceHandler("myHandler", b =>
            {
                b.AddFallback(new FallbackStrategyOptions<HttpResponseMessage>()
                {
                    FallbackAction = _ => Outcome.FromResultAsValueTask(new HttpResponseMessage(HttpStatusCode.ServiceUnavailable))
                })
                .AddConcurrencyLimiter(100)
                .AddRetry(new HttpRetryStrategyOptions())
                .AddCircuitBreaker(new HttpCircuitBreakerStrategyOptions())
                .AddTimeout(new HttpTimeoutStrategyOptions())
                .AddRateLimiter(new RateLimiterStrategyOptions()
                {
                    DefaultRateLimiterOptions = new ConcurrencyLimiterOptions()
                    {
                        PermitLimit = 2,
                    }
                });
            });

            return clientBuilder;
        }
    }
}

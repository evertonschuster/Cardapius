using BuildingBlock.Observability.ElasticStack.Middleware;
using BuildingBlock.Observability.ElasticStack.Options;
using BuildingBlock.Observability.ElasticStack.Traces;
using BuildingBlock.Observability.Traces;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Observability.ElasticStack.Extensions
{
    public static class ElasticStackExtension
    {

        public static void AddObservability(this WebApplicationBuilder builder, Action<ObservabilityOption> actionConfigure)
        {
            var options = builder.Services.RegisterOptions(actionConfigure);
            builder.Services.AddScoped<ITracer, ElasticTracer>();

            builder.AddElasticStackLoggin(options);
        }

        public static void UseObservability(this IApplicationBuilder builder)
        {
            var configuration = builder.ApplicationServices.GetService<IConfiguration>()!;
            var elasticApmConfiguration = configuration.GetSection("Observability:ElasticApm");

            if (elasticApmConfiguration.GetValue("Enabled", true))
            {
                builder.UseMiddleware<RequestSerilLogMiddleware>();
                builder.UseElasticStackTracking(elasticApmConfiguration);
            }
        }



        private static ObservabilityOption RegisterOptions(this IServiceCollection services, Action<ObservabilityOption> actionConfigure)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (actionConfigure is null)
            {
                throw new ArgumentNullException(nameof(actionConfigure));
            }

            var options = new ObservabilityOption();
            actionConfigure(options);

            return options;
        }
    }
}

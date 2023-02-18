using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace BuildingBlocks.Observability.ElasticStack.Extensions
{
    internal static class ElasticStackApmExtension
    {
        public static void UseElasticStackTracking(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseAllElasticApm(configuration);
        }
    }
}

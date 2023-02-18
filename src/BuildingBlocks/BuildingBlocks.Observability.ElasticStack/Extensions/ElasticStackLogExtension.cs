using BuildingBlocks.Observability.ElasticStack.Options;
using BuildingBlocks.Observability.Exceptions;
using Elastic.Apm.SerilogEnricher;
using Elastic.CommonSchema.Serilog;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace BuildingBlocks.Observability.ElasticStack.Extensions
{
    internal static class ElasticStackLogExtension
    {
        public static string EnvironmentName { get; } = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? throw new EnvironmentNotSetException();
        private static string ApplicationName { get; } = (Assembly.GetEntryAssembly()?.GetName().Name ?? "unknow").ToLowerInvariant().Replace(".", "_");



        public static void AddElasticStackLoggin(this WebApplicationBuilder builder, ObservabilityOption option)
        {
            var elasticsearchSinkOptions = ConfigureElasticSink(option.Elastic.URI);
            var customEnvironment = $"{ApplicationName} - {EnvironmentName}";

            //https://www.elastic.co/guide/en/apm/agent/dotnet/master/serilog.html
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithElasticApmCorrelationInfo()
                .Enrich.WithProperty("Environment", customEnvironment)
                .WriteTo.Debug()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(elasticsearchSinkOptions)
                .CreateLogger();


            builder.Logging.ClearProviders();
            builder.Host.UseSerilog(Log.Logger, true);
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(string uri)
        {
            var environmentName = EnvironmentName?.ToLower().Replace(".", "-");

            var elasticsearchUri = new Uri(uri);
            return new ElasticsearchSinkOptions(elasticsearchUri)
            {
                CustomFormatter = new EcsTextFormatter(),
                AutoRegisterTemplate = true,
                IndexFormat = $"{ApplicationName}-{environmentName}",
            };
        }
    }
}

using OpenTelemetry;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Hexata.Worker.Extensions
{
    public static class OpenTelemetryExtension
    {
        public static IHostApplicationBuilder AddObservability(this IHostApplicationBuilder builder)
        {
            var serviceName = builder.Environment.ApplicationName;

            if (string.IsNullOrWhiteSpace(serviceName))
            {
                throw new InvalidOperationException("O nome do serviço (ServiceName) não foi configurado.");
            }

            builder.Logging.AddOpenTelemetry(logging =>
            {
                logging.IncludeFormattedMessage = true;
                logging.IncludeScopes = true;
                logging
                     .SetResourceBuilder(
                        ResourceBuilder.CreateDefault()
                            .AddService(serviceName)
                    );
                //.AddConsoleExporter();
            });

            builder.Services
                .AddOpenTelemetry()
                .ConfigureResource(resource => resource.AddService(serviceName))
                .WithMetrics(metrics =>
                {
                    metrics
                        .AddAspNetCoreInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddRuntimeInstrumentation();
                    //.AddConsoleExporter();
                })
                .WithTracing(tracing =>
                {
                    tracing
                        .AddSource(serviceName)
                        .AddAspNetCoreInstrumentation()
                        //.AddGrpcClientInstrumentation()
                        .AddHttpClientInstrumentation()
                        .AddSqlClientInstrumentation();
                    //.AddConsoleExporter();
                });

            builder.AddOpenTelemetryExporters();


            return builder;
        }

        private static TBuilder AddOpenTelemetryExporters<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            var useOtlpExporter = !string.IsNullOrWhiteSpace(builder.Configuration["OTEL_EXPORTER_OTLP_ENDPOINT"]);

            if (useOtlpExporter)
            {
                builder.Services
                    .AddOpenTelemetry()
                    .UseOtlpExporter();
            }

            // Uncomment the following lines to enable the Azure Monitor exporter (requires the Azure.Monitor.OpenTelemetry.AspNetCore package)
            //if (!string.IsNullOrEmpty(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]))
            //{
            //    builder.Services.AddOpenTelemetry()
            //       .UseAzureMonitor();
            //}

            return builder;
        }
    }

}

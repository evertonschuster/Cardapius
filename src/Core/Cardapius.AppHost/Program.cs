var builder = DistributedApplication.CreateBuilder(args);

var otlpCollector = builder.AddOpenTelemetryCollector("otlp");

var sentinelApi = builder
    .AddProject<Projects.Sentinel_Api>("SentinelApi")
    .WithReference(otlpCollector);

var administrationApi = builder
    .AddProject<Projects.Administration_Api>("AdministrationApi")
    .WithReference(sentinelApi)
    .WithReference(otlpCollector);

builder
    .AddNpmApp("erp-frontend", "../../Apps/erp")
    .WithReference(administrationApi)
    .WithReference(sentinelApi)
    .WithReference(otlpCollector)
    .WithHttpEndpoint(envName: "PORT")
    .WithEnvironment("VITE_OTEL_COLLECTOR_URL", otlpCollector.GetEndpoint("otlp-http"))
    .WithEnvironment("VITE_OTEL_SERVICE_NAME", "erp-frontend");

await builder.Build().RunAsync();

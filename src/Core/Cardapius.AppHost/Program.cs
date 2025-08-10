var builder = DistributedApplication.CreateBuilder(args);

var sentinelDb = builder.AddContainer("sentinel-db", "postgres:16")
    .WithEnvironment("POSTGRES_USER", "postgres")
    .WithEnvironment("POSTGRES_PASSWORD", "postgres")
    .WithEnvironment("POSTGRES_DB", "sentinel")
    .WithPortBinding(5432, 5432);

var sentinelApi = builder
    .AddProject<Projects.Sentinel_Api>("SentinelApi")
    .WithEnvironment("ConnectionStrings__Default", "Host=sentinel-db;Port=5432;Database=sentinel;Username=postgres;Password=postgres")
    .WithReference(sentinelDb);

var administrationApi = builder
    .AddProject<Projects.Administration_Api>("AdministrationApi")
    .WithReference(sentinelApi);

builder.Build().Run();

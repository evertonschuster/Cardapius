var builder = DistributedApplication.CreateBuilder(args);

var sentinelApi = builder
    .AddProject<Projects.Sentinel_Api>("SentinelApi");

var administrationApi = builder
    .AddProject<Projects.Administration_Api>("AdministrationApi")
    .WithReference(sentinelApi);

builder.Build().Run();

using Cardapius.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

var cardapiusDb = builder.AddCardapiusDatabase();


var sentinelApi = builder
    .AddProject<Projects.Sentinel_Api>("SentinelApi")
    .WithReference(cardapiusDb, "SentinelDb")
    .WaitFor(cardapiusDb);

var administrationApi = builder
    .AddProject<Projects.Administration_Api>("AdministrationApi")
    .WithReference(sentinelApi)
    .WithReference(cardapiusDb, "AdministrationDb")
    .WaitFor(cardapiusDb);

await builder.Build().RunAsync();
var builder = DistributedApplication.CreateBuilder(args);


var administrationApi = builder
    .AddProject<Projects.Administration_Api>("AdministrationApi");

builder.Build().Run();

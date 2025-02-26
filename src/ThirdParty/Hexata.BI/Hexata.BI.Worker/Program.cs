using Hexata.BI.Application.Extensions;
using Hexata.BI.Infrastructure.Firebird;
using Hexata.BI.Worker;
using Hexata.Worker.Extensions;

var builder = Host.CreateApplicationBuilder(args);


builder.AddObservability();
builder.AddHangFire();
builder.AddWorkflowApp();


var firebirdConnectionString = builder.Configuration.GetConnectionString("FirebirdConnection")!;
builder.Services.AddFirebird(firebirdConnectionString);



builder.Services.AddHostedService<Worker>();
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Hexata.BI";
});


var host = builder.Build();
host.UseWorkflow();
host.UseHangFire();


await host.RunAsync();

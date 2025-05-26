using Hexata.BI.Application.Extensions;
using Hexata.BI.Infrastructure.Firebird;
using Hexata.BI.Worker;
using Hexata.Infrastructure.Mongo;
using Hexata.Infrastructure.Postgres;
using Hexata.Infrastructure.SqlLite;
using Hexata.Worker.Extensions;

var builder = Host.CreateApplicationBuilder(args);


builder.AddObservability();
builder.AddMongo();
builder.AddPostgres();
builder.AddHangFire();
builder.AddWorkflowApp();


var firebirdConnectionString = builder.Configuration.GetConnectionString("FirebirdConnection")!;
builder.Services.AddFirebird(firebirdConnectionString);
builder.AddSqlLite();



builder.Services.AddHostedService<Worker>();
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Hexata.BI";
});

var host = builder.Build();

host.ApplyPostgresMigrations();
host.ApplySqlLiteMigrations();

await host.RunAsync();

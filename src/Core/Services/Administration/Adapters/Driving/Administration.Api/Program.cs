using Administration.Application;
using Administration.Infra.DataBase.EntityFramework.Extensions;
using BuildingBlock.Api.Extensions;



var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

//BuildingBlocks
builder.AddServiceDefaults();

// Authentication against Sentinel
//builder.Services.AddAuthentication("Bearer")
//    .AddJwtBearer("Bearer", options =>
//    {
//        options.Authority = configuration["Sentinel:Authority"];
//        options.Audience = configuration["Sentinel:Audience"];
//        options.RequireHttpsMetadata = false;
//    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdministrationManager", p => p.RequireRole("Administration.Manager"));
});

//Current service
builder.Services.AddApplication();
builder.Services.AddInfraDataBaseEntityFramework(configuration);



var app = builder.Build();

//BuildingBlocks
app.UseServiceDefaults();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.Run();


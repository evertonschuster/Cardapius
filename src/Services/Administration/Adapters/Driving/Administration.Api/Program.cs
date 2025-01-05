using Administration.Application;
using Administration.Infra.DataBase.EntityFramework.Extensions;
using BuildingBlock.Api.Extensions;



var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;


//BuildingBlocks
builder.AddServiceDefaults();


//Current service
builder.Services.AddApplication();
builder.Services.AddInfraDataBaseEntityFramework(configuration);



var app = builder.Build();

//BuildingBlocks
app.UseServiceDefaults();


app.UseHttpsRedirection();
app.UseAuthorization();



app.Run();


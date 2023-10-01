using Administration.Application;
using Administration.Infra.DataBase.EntityFramework.Extensions;
using BuildingBlock.Api.Modules;

namespace Administration.Api.Extensions
{
    public static class AdministrationServiceExtensions
    {
        public static WebApplicationBuilder AddAdministrationService(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Services.AddApplication();
            builder.Services.AddInfraDataBaseEntityFramework(configuration);
            builder.Services.AddModule<Startup>("administration", configuration);

            return builder;
        }
    }
}

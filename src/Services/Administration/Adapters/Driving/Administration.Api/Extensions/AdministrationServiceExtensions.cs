using System.Reflection;
using Administration.Application;
using Administration.Infra.DataBase.EntityFramework.Extensions;

namespace Administration.Api.Extensions
{
    public static class AdministrationServiceExtensions
    {
        public static WebApplicationBuilder AddAdministrationService(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Services.AddApplication();
            builder.Services.AddInfraDataBaseEntityFramework(configuration);

            return builder;
        }

        public static IMvcBuilder AddAdministrationService(this IMvcBuilder builder)
        {
            builder.AddApplicationPart(Assembly.GetExecutingAssembly());

            return builder;
        }
    }
}

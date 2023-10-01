using Administration.Application;
using Administration.Infra.DataBase.EntityFramework.Extensions;

namespace Administration.Api
{
    public class Startup : BuildingBlock.Api.IStartup
    {
        public void Configure(IApplicationBuilder app, IConfiguration configuration)
        {
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            services.AddInfraDataBaseEntityFramework(configuration);
        }
    }
}

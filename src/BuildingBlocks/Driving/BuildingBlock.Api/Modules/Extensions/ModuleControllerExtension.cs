using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BuildingBlock.Api.Modules.Extensions
{
    public static class ModuleControllerExtension
    {
        public static IMvcBuilder AddApplicationControllerModules(this IMvcBuilder builder)
        {
            // Register a convention allowing to us to prefix routes to modules.
            builder.Services.AddTransient<IPostConfigureOptions<MvcOptions>, ModuleRoutingMvcOptionsPostConfigure>();

            return builder.ConfigureApplicationPartManager(apm =>
            {
                apm.ApplicationParts.Clear();
                apm.FeatureProviders.Add(new InternalControllerFeatureProvider());
            });
        }

        public static IApplicationBuilder UseApplicationControllerModules(this IApplicationBuilder app)
        {
            var modules = app.ApplicationServices.GetServices<Module>();
            var configuration = app.ApplicationServices.GetService<IConfiguration>()!;

            //Adds endpoints defined in modules
            foreach (var module in modules)
            {
                module.Startup.Configure(app, configuration);
            }

            return app;
        }
    }
}

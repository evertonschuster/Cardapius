namespace Sentinel.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddAppSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new() { Title = "Sentinel API", Version = "v1" });
            });

            return services;
        }

        public static IApplicationBuilder UseAppSwagger(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(o =>
                {
                    o.SwaggerEndpoint("/swagger/v1/swagger.json", "Sentinel API v1");
                    o.OAuthClientId("swagger");
                    o.OAuthUsePkce();
                });
            }

            return app;
        }
    }
}

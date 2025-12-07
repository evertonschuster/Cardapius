namespace Cardapius.AppHost
{
    public static class CardapiusExtensions
    {
        public static IResourceBuilder<PostgresDatabaseResource> AddCardapiusDatabase(this IDistributedApplicationBuilder builder, string resourceName = "Cardapius", int port = 5432)
        {
            var userPostgres = builder.AddParameter("postgres-user", "postgres");
            var senhaPostgres = builder.AddParameter("postgres-password", "postgres");

            var postgresServer = builder
                .AddPostgres("postgres-1")
                .WithUserName(userPostgres)
                .WithPassword(senhaPostgres)
                .WithHostPort(port) 
                .WithContainerName("cardapius-postgres") 
                .WithDataVolume("Cardapius-postgres") 
                .WithLifetime(ContainerLifetime.Persistent);

            return postgresServer.AddDatabase(resourceName);
        }
    }
}

using FirebirdSql.Data.FirebirdClient;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Hexata.BI.Infrastructure.Firebird
{
    public static class InjectionExtensions
    {
        public static void AddFirebird(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IDbConnection>(sp =>
            {
                return new FbConnection(connectionString);
            });
        }
    }
}

using Dapper;
using FirebirdSql.Data.FirebirdClient;
using Hexata.BI.Application.Repositories;
using Hexata.Infrastructure.Firebird.Repositories;
using Hexata.Infrastructure.Firebird.SqlMappers;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Hexata.BI.Infrastructure.Firebird
{
    public static class InjectionExtensions
    {
        public static void AddFirebird(this IServiceCollection services, string connectionString)
        {
            SqlMapper.AddTypeHandler(new AuxiliarySpeciesTypeHandler());
            SqlMapper.AddTypeMap(typeof(List<string>), DbType.String);
            SqlMapper.AddTypeHandler(new SanitizingStringHandler());
            SqlMapper.AddTypeMap(typeof(string), DbType.String);


            services.AddSingleton<IDbConnection>(sp =>
            {
                return new FbConnection(connectionString);
            });

            services.AddScoped<IErpSaleRepository, ErpSaleRepository>();
            services.AddScoped<IErpCustomerRepository, ErpCustomerRepository>();
        }
    }
}

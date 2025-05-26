using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hexata.Infrastructure.Postgres
{
    public class HexataDbContextFactory : IDesignTimeDbContextFactory<HexataDbContext>
    {
        HexataDbContext IDesignTimeDbContextFactory<HexataDbContext>.CreateDbContext(string[] args)
        {
            var connectionString = "Host=localhost;Database=hexata;Username=usuario;Password=senha";

            var optionsBuilder = new DbContextOptionsBuilder<HexataDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new HexataDbContext(optionsBuilder.Options);
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Administration.Infra.DataBase.EntityFramework
{
    internal class AdministrationDbContextFactory : IDesignTimeDbContextFactory<AdministrationDbContext>
    {
        /// <summary>
        /// Creates and configures a new instance of <see cref="AdministrationDbContext"/> for design-time operations.
        /// </summary>
        /// <param name="args">Arguments passed to the context factory, typically from tooling.</param>
        /// <returns>A configured <see cref="AdministrationDbContext"/> instance connected to the specified PostgreSQL database.</returns>
        public AdministrationDbContext CreateDbContext(string[] args)
        {
            Console.WriteLine($"Creating AdministrationDbContext... {string.Join(" ", args)}");
            var optionsBuilder = new DbContextOptionsBuilder<AdministrationDbContext>();
            optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=Cardapius;");

            return new AdministrationDbContext(null, optionsBuilder.Options);
        }
    }
}

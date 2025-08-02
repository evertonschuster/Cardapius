using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Administration.Infra.DataBase.EntityFramework
{
    internal class AdministrationDbContextFactory : IDesignTimeDbContextFactory<AdministrationDbContext>
    {
        public AdministrationDbContext CreateDbContext(string[] args)
        {
            Console.WriteLine($"Creating AdministrationDbContext... {string.Join(" ", args)}");
            var optionsBuilder = new DbContextOptionsBuilder<AdministrationDbContext>();
            optionsBuilder.UseNpgsql("User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=Cardapius;");

            return new AdministrationDbContext(null!, optionsBuilder.Options);
        }
    }
}

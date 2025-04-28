using Hexata.BI.Application.DataBaseSyncs;
using Hexata.BI.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Hexata.Infrastructure.SqlLite
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ServiceState> ServiceStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ServiceState>(ConfigureServiceState);
        }


        private void ConfigureServiceState(EntityTypeBuilder<ServiceState> builder)
        {
            var settigs = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            builder.Property(p => p.State)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v, settigs),
                    v => JsonConvert.DeserializeObject<SyncDto>(v, settigs))
                .HasColumnType("TEXT");
        }
    }
}

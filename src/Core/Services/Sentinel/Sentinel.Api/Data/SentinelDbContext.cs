using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sentinel.Api.Models;

namespace Sentinel.Api.Data;

public class SentinelDbContext(DbContextOptions<SentinelDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
{
    public DbSet<TokenLifetimeOption> TokenLifetimeOptions => Set<TokenLifetimeOption>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("Sentine");

        modelBuilder.Entity<TokenLifetimeOption>(entity =>
        {
            entity.ToTable("TokenLifetimeOptions");
            entity.Property(e => e.AllowedScopes).HasColumnType("text");
        });
    }
}


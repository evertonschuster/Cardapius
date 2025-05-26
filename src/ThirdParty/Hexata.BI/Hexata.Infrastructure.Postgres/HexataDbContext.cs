using Hexata.BI.Application.DataBaseSyncs.Customers.Models;
using Hexata.BI.Application.DataBaseSyncs.Sales.Models;
using Hexata.Infrastructure.Postgres.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection;

namespace Hexata.Infrastructure.Postgres
{
    internal class HexataDbContext(DbContextOptions<HexataDbContext> options) : DbContext(options)
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderItemAuxiliarySpecie> OrderItemAuxiliarySpecies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.HasIndex(o => o.Date);
                entity.HasIndex(o => o.CustomerId);

                modelBuilder.Entity<Order>()
                    .OwnsOne(o => o.Address)
                    .ToJson()
                    .HasJsonPropertyName("endereco");

                entity
                    .OwnsOne(c => c.Localization)
                    .ToJson();
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(i => i.Id);
                entity.HasIndex(i => i.OrderId);
                entity.HasIndex(i => new { i.OrderId, i.Product });
                entity.HasOne<Order>()
                      .WithMany(o => o.Items)
                      .HasForeignKey(i => i.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<OrderItemAuxiliarySpecie>(entity =>
            {
                entity.HasKey(s => new { s.OrderId, s.OrderItemId, s.AuxiliarySpecie });
                entity.HasIndex(s => s.OrderItemId);
                entity.HasOne<OrderItem>()
                      .WithMany()
                      .HasForeignKey(s => s.OrderItemId)
                      .OnDelete(DeleteBehavior.Cascade);
            });



            // Lista dos tipos que devem ter mapeamento por DisplayName
            var tiposComDisplayName = new[]
            {
                typeof(Customer),
                typeof(Order),
                typeof(OrderItem),
                typeof(OrderItemAuxiliarySpecie)
            };

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entity.ClrType;
                if (clrType == null || !tiposComDisplayName.Contains(clrType))
                    continue;

                foreach (var property in clrType.GetProperties())
                {
                    // Só mapeia se for propriedade escalar
                    var efProperty = entity.FindProperty(property.Name);
                    if (efProperty == null)
                        continue;

                    var displayAttr = property.GetCustomAttribute<DisplayNameAttribute>();
                    if (displayAttr != null && !string.IsNullOrEmpty(displayAttr.DisplayName))
                    {
                        modelBuilder.Entity(clrType)
                            .Property(property.Name)
                            .HasColumnName(displayAttr.DisplayName);
                    }
                }
            }


        }
    }
}
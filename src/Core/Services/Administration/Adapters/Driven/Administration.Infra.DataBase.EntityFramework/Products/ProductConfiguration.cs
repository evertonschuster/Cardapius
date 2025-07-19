using Administration.Application.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Administration.Infra.DataBase.EntityFramework.Products
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.OwnsMany(p => p.Images, images =>
            {
                images.WithOwner().HasForeignKey("ProductId");
                images.ToTable("ProductImages");
            });

            builder.OwnsOne(p => p.Flavor, f =>
            {
                f.OwnsMany(x => x.Items, items =>
                {
                    items.WithOwner().HasForeignKey("ProductFlavorId");
                    items.Property<Guid>("Id").ValueGeneratedOnAdd();
                    items.HasKey("Id");
                    items.Property(e => e.Description).HasMaxLength(256);

                    items.ToTable("ProductFlavorItems");
                });
            });

            builder.OwnsOne(p => p.Additional, f =>
            {
                f.OwnsMany(x => x.Items, items =>
                {
                    items.WithOwner().HasForeignKey("ProductAdditionalId");
                    items.Property<Guid>("Id").ValueGeneratedOnAdd();
                    items.HasKey("Id");
                    items.Property(e => e.Description).HasMaxLength(256);

                    items.ToTable("ProductAdditionalItems");
                });
            });

            builder.OwnsOne(p => p.Preference, f =>
            {
                f.OwnsMany(x => x.Items, items =>
                {
                    items.WithOwner().HasForeignKey("ProductPreferenceId");
                    items.Property<Guid>("Id").ValueGeneratedOnAdd();
                    items.HasKey("Id");
                    items.Property(e => e.Description).HasMaxLength(256);

                    items.ToTable("ProductPreferenceItems");
                });
            });

            builder.OwnsOne(e => e.ServesManyPeople);

            builder
               .HasMany(p => p.SideDishes)
               .WithMany()
               .UsingEntity<Dictionary<string, object>>(
                   "ProductSideDishes",   
                   right => right
                       .HasOne<Product>()
                       .WithMany()
                       .HasForeignKey("SideDishId")
                       .OnDelete(DeleteBehavior.Cascade),
                   left => left
                       .HasOne<Product>()
                       .WithMany()
                       .HasForeignKey("ProductId")
                       .OnDelete(DeleteBehavior.Cascade),
                   join =>
                   {
                       join.HasKey("ProductId", "SideDishId");
                       join.ToTable("ProductSideDishes");
                   }
               );
        }
    }
}

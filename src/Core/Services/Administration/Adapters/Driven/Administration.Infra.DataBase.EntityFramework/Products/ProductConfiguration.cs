using Administration.Domain.Products.Entities;
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
                f.WithOwner().HasForeignKey("ProductId");
                f.ToTable("ProductFlavors");

                f.OwnsMany(x => x.Items, items =>
                {
                    items.Property<Guid>("Id").ValueGeneratedOnAdd();
                    items.HasKey("Id");
                    items.Property(e => e.Description).HasMaxLength(256);

                    items.WithOwner().HasForeignKey("ProductFlavorId");
                    items.ToTable("ProductFlavorItems");
                });
            });

            builder.OwnsOne(p => p.Additional, f =>
            {
                f.WithOwner().HasForeignKey("ProductId");
                f.ToTable("ProductAdditionals");

                f.OwnsMany(x => x.Items, items =>
                {
                    items.Property<Guid>("Id").ValueGeneratedOnAdd();
                    items.HasKey("Id");
                    items.Property(e => e.Description).HasMaxLength(256);

                    items.WithOwner().HasForeignKey("ProductAdditionalId");
                    items.ToTable("ProductAdditionalItems");
                });
            });

            builder.OwnsOne(p => p.Preference, f =>
            {
                f.WithOwner().HasForeignKey("ProductId");
                f.ToTable("ProductPreferences");

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
            builder.Navigation(p => p.ServesManyPeople).IsRequired();


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

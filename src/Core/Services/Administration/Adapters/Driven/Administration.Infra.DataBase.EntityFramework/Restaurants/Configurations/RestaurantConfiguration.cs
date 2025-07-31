using Administration.Domain.Restaurants.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Administration.Infra.DataBase.EntityFramework.Restaurants.Configurations
{
    internal class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
    {
        /// <summary>
        /// Configures the Restaurant entity to own a single Address value object in the Entity Framework model.
        /// </summary>
        /// <param name="builder">The builder used to configure the Restaurant entity type.</param>
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.OwnsOne(e => e.Address);
        }
    }
}

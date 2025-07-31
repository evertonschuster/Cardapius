using BuildingBlock.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BuildingBlock.Infra.DataBase.EntityFramework.Entities
{
    internal class OutboxMessageEntityConfiguration : IEntityTypeConfiguration<OutboxMessageEntity>
    {
        /// <summary>
        /// Configures the entity mapping for <see cref="OutboxMessageEntity"/>, specifying property constraints such as maximum lengths and required fields.
        /// </summary>
        /// <param name="builder">The builder used to configure the <see cref="OutboxMessageEntity"/> entity type.</param>
        public void Configure(EntityTypeBuilder<OutboxMessageEntity> builder)
        {
            builder.Property(e => e.EntityType)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(e => e.EventType)
                .HasMaxLength(256)
                .IsRequired();


            builder.Property(e => e.SynReceivedFrom)
                .HasMaxLength(64)
                .IsRequired(false);
        }
    }
}

using BuildingBlock.Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BuildingBlock.Infra.DataBase.EntityFramework.Entities
{
    internal class OutboxMessageEntityConfiguration : IEntityTypeConfiguration<OutboxMessageEntity>
    {
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

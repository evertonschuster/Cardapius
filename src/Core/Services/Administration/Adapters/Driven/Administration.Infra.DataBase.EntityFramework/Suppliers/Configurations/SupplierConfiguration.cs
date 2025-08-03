using Administration.Domain.Suppliers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Administration.Infra.DataBase.EntityFramework.Suppliers.Configurations;

internal class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.OwnsOne(e => e.Address);
        builder.OwnsOne(e => e.BankInformation);
        builder.OwnsOne(e => e.Documentations);
    }
}

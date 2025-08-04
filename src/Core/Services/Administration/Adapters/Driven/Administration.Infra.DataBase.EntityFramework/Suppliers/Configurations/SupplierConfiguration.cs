using Administration.Domain.Suppliers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Administration.Infra.DataBase.EntityFramework.Suppliers.Configurations;

internal class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasIndex(s => s.CpfCnpj);
        builder.HasIndex(s => s.Status);
        builder.HasIndex(s => s.LegalName);
        
        builder.Property(s => s.CpfCnpj).IsRequired().HasMaxLength(20);
        builder.Property(s => s.LegalName).IsRequired().HasMaxLength(200);
        builder.Property(s => s.TradeName).HasMaxLength(200);

        builder.OwnsOne(e => e.Address);
        builder.OwnsOne(e => e.BankInformation);
        builder.OwnsOne(e => e.SupportingDocuments);
    }
}

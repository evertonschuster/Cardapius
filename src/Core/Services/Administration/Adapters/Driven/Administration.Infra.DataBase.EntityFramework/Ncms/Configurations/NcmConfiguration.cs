using Administration.Domain.Ncms.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Administration.Infra.DataBase.EntityFramework.Ncms.Configurations;

internal class NcmConfiguration : IEntityTypeConfiguration<Ncm>
{
    public void Configure(EntityTypeBuilder<Ncm> builder)
    {
        builder.ToTable("Ncms");

        builder.HasIndex(e => e.Code.Code);
        builder.Property(e => e.Description).IsRequired().HasMaxLength(512);

        builder.OwnsOne(e => e.Code, code =>
        {
            code.Property(c => c.Code).HasColumnName("Ncm").HasMaxLength(20);
        });

        builder.OwnsOne(e => e.TaxationProfile, taxation =>
        {
            taxation.Property(t => t.TaxationDescription).HasColumnName("Tributacao").HasMaxLength(128).IsRequired();
            taxation.Property(t => t.FixedRate).HasColumnName("AliquotaFixa");
        });

        builder.OwnsOne(e => e.TaxEstimation, tax =>
        {
            tax.Property(t => t.National).HasColumnName("TributoEstimadoNac");
            tax.Property(t => t.Imported).HasColumnName("TributoEstimadoImp");
            tax.Property(t => t.State).HasColumnName("TributoEstimadoEst");
            tax.Property(t => t.Municipal).HasColumnName("TributoEstimadoMun");
        });

        builder.OwnsOne(e => e.TableVersion, table =>
        {
            table.Property(t => t.Version).HasColumnName("VersaoTabela").HasMaxLength(32);
            table.Property(t => t.FederativeUnit).HasColumnName("UfTabela").HasMaxLength(2);
        });

        builder.OwnsOne(e => e.Validity, validity =>
        {
            validity.Property(v => v.Start).HasColumnName("VigenciaI");
            validity.Property(v => v.End).HasColumnName("VigenciaF");
        });

        builder.OwnsOne(e => e.Classification, classification =>
        {
            classification.Property(c => c.Group).HasColumnName("Grupo").HasMaxLength(64);
            classification.Property(c => c.Subgroup).HasColumnName("Subgrupo").HasMaxLength(64);
            classification.Property(c => c.Section).HasColumnName("Sessao").HasMaxLength(64);
            classification.Property(c => c.SectionDetail).HasColumnName("SessaoDetalhe").HasMaxLength(64);
        });

        builder.OwnsOne(e => e.TaxableUnit, taxableUnit =>
        {
            taxableUnit.Property(t => t.Unit).HasColumnName("UnidadeTributada").HasMaxLength(64);
            taxableUnit.Property(t => t.Description).HasColumnName("UnidadeDescricao").HasMaxLength(256);
        });

        builder.OwnsOne(e => e.BenefitCode, benefit =>
        {
            benefit.Property(b => b.Code).HasColumnName("CodigoBeneficio").HasMaxLength(64);
        });
    }
}

using BuildingBlock.Domain.ValueObjects.Contact;
using BuildingBlock.Domain.ValueObjects.Location;
using BuildingBlock.Domain.ValueObjects.Media;
using BuildingBlock.Domain.ValueObjects.Products;
using BuildingBlock.Domain.ValueObjects.Time;
using BuildingBlock.Infra.Domain.ValueObjects.EFCore.Emails;
using BuildingBlock.Infra.Domain.ValueObjects.EFCore.PersonNames;
using BuildingBlock.Infra.Domain.ValueObjects.EFCore.Phones;
using BuildingBlock.Infra.Domain.ValueObjects.EFCore.Products;
using BuildingBlock.Infra.Domain.ValueObjects.EFCore.Times;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Infra.Domain.ValueObjects.EFCore.Extensions
{
    public static class DomainDataExtension
    {
        public static void AddApplicationDomainDataEFCoreConvert(this ModelBuilder modelBuilder)
        {
            modelBuilder.Owned<Address>();
            modelBuilder.Owned<Image>();
            modelBuilder.Owned<SalePrice>();
            modelBuilder.Owned<ProductionPrice>();

            var imageType = modelBuilder.Model.FindEntityType(typeof(Image));
            if (imageType != null)
            {
                imageType.FindProperty(nameof(Image.Uri))?.SetMaxLength(Image.MaxUriLength);
                imageType.FindProperty(nameof(Image.AlternativeText))?.SetMaxLength(Image.MaxAlternativeTextLength);
                imageType.FindProperty(nameof(Image.ThumbnailUri))?.SetMaxLength(Image.MaxThumbnailUriLength);
                imageType.FindProperty(nameof(Image.BlurHash))?.SetMaxLength(Image.MaxBlurHashLength);
            }
        }

        public static void AddApplicationDomainDataEFCoreConvert(this ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder
                .Properties<Email>()
                .HaveConversion<EmailConverter>()
                .HaveMaxLength(64);

            configurationBuilder
                .Properties<Phone>()
                .HaveConversion<PhoneConvert>()
                .HaveMaxLength(32);

            configurationBuilder
                .Properties<PersonName>()
                .HaveConversion<PersonNameConvert>()
                .HaveMaxLength(32);

            configurationBuilder
                .Properties<ProductName>()
                .HaveConversion<ProductNameConvert>()
                .HaveMaxLength(32);

            configurationBuilder
                .Properties<PreparationTime>()
                .HaveConversion<PreparationTimeConvert>();

        }
    }
}
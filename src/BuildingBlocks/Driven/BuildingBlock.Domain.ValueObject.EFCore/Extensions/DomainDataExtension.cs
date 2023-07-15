using BuildingBlock.Domain.ValueObjects.Emails;
using BuildingBlock.Domain.ValueObjects.PersonNames;
using BuildingBlock.Domain.ValueObjects.Phones;
using BuildingBlock.Infra.Domain.ValueObjects.EFCore.Emails;
using BuildingBlock.Infra.Domain.ValueObjects.EFCore.PersonNames;
using BuildingBlock.Infra.Domain.ValueObjects.EFCore.Phones;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.Infra.Domain.ValueObjects.EFCore.Extensions
{
    public static class DomainDataExtension
    {
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
        }
    }
}
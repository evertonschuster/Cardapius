using BuildingBlock.Domain.ValueObjects.Contact;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlock.Infra.Domain.ValueObjects.EFCore.PersonNames
{
    internal class PersonNameConvert : ValueConverter<PersonName, string>
    {
        public PersonNameConvert() : base(v => v.Value, v => PersonName.Parse(v).Value)
        {
        }
    }
}

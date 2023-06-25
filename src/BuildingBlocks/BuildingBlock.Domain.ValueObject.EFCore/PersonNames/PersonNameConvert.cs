using BuildingBlock.Domain.ValueObjects.PersonNames;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlock.Domain.ValueObjects.EFCore.PersonNames
{
    internal class PersonNameConvert : ValueConverter<PersonName, string>
    {
        public PersonNameConvert() : base(v => v.Value, v => PersonName.Parse(v))
        {
        }
    }
}

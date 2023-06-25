using BuildingBlock.Domain.ValueObjects.Phones;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlock.Domain.ValueObjects.EFCore.Phones
{
    internal class PhoneConvert : ValueConverter<Phone, string>
    {
        public PhoneConvert() : base(v => v.Value, v => Phone.Parse(v))
        {
        }
    }
}

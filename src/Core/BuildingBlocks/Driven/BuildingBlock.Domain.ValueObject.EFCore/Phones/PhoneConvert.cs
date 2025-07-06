using BuildingBlock.Domain.ValueObjects.Contact;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlock.Infra.Domain.ValueObjects.EFCore.Phones
{
    internal class PhoneConvert : ValueConverter<Phone, string>
    {
        public PhoneConvert() : base(v => v.Value, v => Phone.Parse(v).Value)
        {
        }
    }
}

using BuildingBlock.Domain.ValueObjects.Emails;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlock.Infra.Domain.ValueObjects.EFCore.Emails
{
    internal class EmailConverter : ValueConverter<Email, string>
    {
        public EmailConverter() : base(v => v.Value, v => Email.Parse(v))
        {
        }
    }
}

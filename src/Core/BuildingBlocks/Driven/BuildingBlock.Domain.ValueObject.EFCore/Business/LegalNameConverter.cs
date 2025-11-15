using BuildingBlock.Domain.ValueObjects.Business;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlock.Infra.Domain.ValueObjects.EFCore.Business
{
    internal class LegalNameConverter : ValueConverter<LegalName, string>
    {
        public LegalNameConverter() : base(v => v.Value, v => LegalName.Parse(v).Value)
        {
        }
    }
}
using BuildingBlock.Domain.ValueObjects.Time;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlock.Infra.Domain.ValueObjects.EFCore.Times
{
    internal class PreparationTimeConvert : ValueConverter<PreparationTime, TimeSpan>
    {
        public PreparationTimeConvert() : base(v => v.Value, v => PreparationTime.Parse(v).Value)
        {
        }
    }
}

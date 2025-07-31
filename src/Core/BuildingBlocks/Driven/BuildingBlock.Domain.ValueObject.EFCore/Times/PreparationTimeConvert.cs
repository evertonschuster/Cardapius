using BuildingBlock.Domain.ValueObjects.Time;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlock.Infra.Domain.ValueObjects.EFCore.Times
{
    internal class PreparationTimeConvert : ValueConverter<PreparationTime, TimeSpan>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PreparationTimeConvert"/> class to convert between <see cref="PreparationTime"/> and <see cref="TimeSpan"/> for Entity Framework Core.
        /// </summary>
        public PreparationTimeConvert() : base(v => v.Value, v => PreparationTime.Parse(v).Value)
        {
        }
    }
}

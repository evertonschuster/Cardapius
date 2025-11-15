using BuildingBlock.Domain.ValueObjects.Business;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlock.Infra.Domain.ValueObjects.EFCore.Business
{
    internal class StateRegistrationConverter : ValueConverter<StateRegistration, string>
    {
        public StateRegistrationConverter() : base(v => v.Value, v => StateRegistration.Parse(v).Value)
        {
        }
    }
}
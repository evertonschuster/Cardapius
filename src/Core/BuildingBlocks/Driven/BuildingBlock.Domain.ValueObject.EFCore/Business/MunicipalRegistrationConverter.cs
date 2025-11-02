using BuildingBlock.Domain.ValueObjects.Business;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlock.Infra.Domain.ValueObjects.EFCore.Business
{
    internal class MunicipalRegistrationConverter : ValueConverter<MunicipalRegistration, string>
    {
        public MunicipalRegistrationConverter() : base(v => v.Value, v => MunicipalRegistration.Parse(v).Value)
        {
        }
    }
}

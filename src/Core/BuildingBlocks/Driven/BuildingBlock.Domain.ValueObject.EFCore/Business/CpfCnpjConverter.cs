using BuildingBlock.Domain.ValueObjects.Business;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlock.Infra.Domain.ValueObjects.EFCore.Business
{
    internal class CpfCnpjConverter : ValueConverter<CpfCnpj, string>
    {
        public CpfCnpjConverter() : base(v => v.Value, v => CpfCnpj.Parse(v).Value)
        {
        }
    }
}
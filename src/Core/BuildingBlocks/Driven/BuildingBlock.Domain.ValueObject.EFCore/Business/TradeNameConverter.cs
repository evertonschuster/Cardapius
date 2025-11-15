using BuildingBlock.Domain.ValueObjects.Business;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuildingBlock.Infra.Domain.ValueObjects.EFCore.Business
{
    internal class TradeNameConverter : ValueConverter<TradeName, string>
    {
        public TradeNameConverter() : base(v => v.Value, v => TradeName.Parse(v).Value)
        {
        }
    }
}
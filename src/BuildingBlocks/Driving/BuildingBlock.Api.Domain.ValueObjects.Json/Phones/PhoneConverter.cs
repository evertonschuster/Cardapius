using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.ValueObjects.Phones;
using Newtonsoft.Json;

namespace BuildingBlock.Api.Domain.ValueObjects.Json.Phones
{
    //TODO: Auto registrarion with ioc
    internal class PhoneConverter : JsonConverter<Phone>
    {
        public override Phone ReadJson(JsonReader reader, Type objectType, Phone existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            try
            {
                var value = reader.Value as string;
                return Phone.Parse(value);
            }
            catch (BusinessException business)
            {
                throw new JsonReaderException(business.Message, business);
            }
        }

        public override void WriteJson(JsonWriter writer, Phone value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}

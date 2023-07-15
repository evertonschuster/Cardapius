using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.ValueObjects.Emails;
using Newtonsoft.Json;

namespace BuildingBlock.Api.Domain.ValueObjects.Json.Emails
{
    //TODO: Auto registrarion with ioc
    internal class EmailJsonConverter : JsonConverter<Email>
    {
        public override Email ReadJson(JsonReader reader, Type objectType, Email existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            try
            {
                var value = reader.Value as string;
                return Email.Parse(value);
            }
            catch (BusinessException business)
            {
                throw new JsonReaderException(business.Message, business);
            }
        }

        public override void WriteJson(JsonWriter writer, Email value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}

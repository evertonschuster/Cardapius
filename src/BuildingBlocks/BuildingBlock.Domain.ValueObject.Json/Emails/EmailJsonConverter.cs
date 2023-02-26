using BuildingBlock.Domain.Exceptions;
using Newtonsoft.Json;

namespace BuildingBlock.Domain.ValueObject.Json.Emails
{
    //TODO: Auto registrarion with ioc
    public class EmailJsonConverter : JsonConverter<ValueObjects.Emails.Email>
    {
        public override ValueObjects.Emails.Email ReadJson(JsonReader reader, Type objectType, ValueObjects.Emails.Email existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            try
            {
                var value = reader.Value as string;
                return ValueObjects.Emails.Email.Parse(value);
            }
            catch (BusinessException business)
            {
                throw new JsonReaderException(business.Message, business);
            }
        }

        public override void WriteJson(JsonWriter writer, ValueObjects.Emails.Email value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}

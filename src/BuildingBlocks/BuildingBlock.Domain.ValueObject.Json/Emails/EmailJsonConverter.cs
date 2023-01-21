using BuildingBlock.Domain.Exceptions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BuildingBlock.Domain.ValueObject.Json.Emails
{
    //TODO: Auto registrarion with ioc
    public class EmailJsonConverter : JsonConverter<ValueObjects.Emails.Email>
    {
        public override ValueObjects.Emails.Email Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                var value = reader.GetString();
                return ValueObjects.Emails.Email.Parse(value);
            }
            catch (BusinessException business)
            {
                throw new BusinessException(business.Message, typeToConvert.Name, business);
            }
        }

        public override void Write(Utf8JsonWriter writer, ValueObjects.Emails.Email value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}

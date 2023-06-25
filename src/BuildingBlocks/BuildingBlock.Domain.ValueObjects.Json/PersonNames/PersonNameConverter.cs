using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.ValueObjects.PersonNames;
using Newtonsoft.Json;

namespace BuildingBlock.Domain.ValueObjects.Json.PersonNames
{
    //TODO: Auto registrarion with ioc
    internal class PersonNameConverter : JsonConverter<PersonName>
    {
        public override PersonName ReadJson(JsonReader reader, Type objectType, PersonName existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            try
            {
                var value = reader.Value as string;
                return PersonName.Parse(value);
            }
            catch (BusinessException business)
            {
                throw new JsonReaderException(business.Message, business);
            }
        }

        public override void WriteJson(JsonWriter writer, PersonName value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}

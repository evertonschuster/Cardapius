//using BuildingBlock.Domain.Exceptions;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//namespace BuildingBlock.Api.Domain.ValueObjects.Json.Address
//{
//    internal class AddressConverter : JsonConverter<BuildingBlock.Domain.ValueObjects.Address.Address>
//    {
//        public override BuildingBlock.Domain.ValueObjects.Address.Address ReadJson(JsonReader reader, Type objectType, BuildingBlock.Domain.ValueObjects.Address.Address existingValue, bool hasExistingValue, JsonSerializer serializer)
//        {
//            if (reader.TokenType == JsonToken.Null)
//                return BuildingBlock.Domain.ValueObjects.Address.Address.Empty;

//            try
//            {
//                var jsonObject = JObject.Load(reader);

//                var street = jsonObject["Street"]?.Value<string>();
//                var number = jsonObject["Number"]?.Value<string>();
//                var complement = jsonObject["Complement"]?.Value<string>();
//                var city = jsonObject["City"]?.Value<string>();
//                var state = jsonObject["State"]?.Value<string>();
//                var zipCode = jsonObject["ZIPCode"]?.Value<string>();

//                var result = BuildingBlock.Domain.ValueObjects.Address.Address.Parse(street, number, complement, city, state, zipCode);

//                if (result.IsSuccess)
//                {
//                    return result.Value;
//                }

//                throw new JsonReaderException($"{string.Join(", ", result.Errors)}");
//            }
//            catch (BusinessException business)
//            {
//                throw new JsonReaderException(business.Message, business);
//            }
//        }

//        public override void WriteJson(JsonWriter writer, BuildingBlock.Domain.ValueObjects.Address.Address address, JsonSerializer serializer)
//        {
//            var jsonObject = new JObject
//            {
//                { "Street", address.Street },
//                { "Number", address.Number },
//                { "Complement", address.Complement },
//                { "City", address.City },
//                { "State", address.State },
//                { "ZIPCode", address.ZIPCode }
//            };

//            jsonObject.WriteTo(writer);
//        }
//    }
//}

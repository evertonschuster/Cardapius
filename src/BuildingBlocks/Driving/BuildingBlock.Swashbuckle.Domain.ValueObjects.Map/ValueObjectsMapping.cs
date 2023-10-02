using BuildingBlock.Domain.ValueObjects.Address;
using BuildingBlock.Domain.ValueObjects.Emails;
using BuildingBlock.Domain.ValueObjects.PersonNames;
using BuildingBlock.Domain.ValueObjects.Phones;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BuildingBlock.Swashbuckle.Domain.ValueObjects.Map
{
    public static class ValueObjectsMapping
    {
        public static SwaggerGenOptions AddDomainMapping(this SwaggerGenOptions options)
        {
            options.MapType<Email>(() => new OpenApiSchema
            {
                Type = "string",
                Title = "Email",
                Description = "Represent a valid email.",
                Example = new OpenApiString(Email.Empty),
            });

            options.MapType<PersonName>(() => new OpenApiSchema
            {
                Type = "string",
                Title = "Name",
                Description = "Represent a valid person name.",
                Example = new OpenApiString(PersonName.Empty),
            });

            options.MapType<Phone>(() => new OpenApiSchema
            {
                Type = "string",
                Title = "Phone",
                Description = "Represent a valid phone number.",
                Example = new OpenApiString(Phone.Empty),
            });
            
            options.MapType<Address>(() => new OpenApiSchema
            {
                Type = "object",
                Title = "Address",
                Description = "Represent a valid address.",
                Properties = new Dictionary<string, OpenApiSchema>
                {
                    { "Street", new OpenApiSchema { Type = "string" } },
                    { "Number", new OpenApiSchema { Type = "string" } },
                    { "Complement", new OpenApiSchema { Type = "string" } },
                    { "City", new OpenApiSchema { Type = "string" } },
                    { "State", new OpenApiSchema { Type = "string" } },
                    { "ZIPCode", new OpenApiSchema { Type = "string" } }
                },
                Example = new OpenApiObject()
                {
                    ["Street"] = new OpenApiString("123 Main St"),
                    ["Number"] = new OpenApiString("Apt 4B"),
                    ["Complement"] = new OpenApiString("Building XYZ"),
                    ["City"] = new OpenApiString("Example Ville"),
                    ["State"] = new OpenApiString("EX"),
                    ["ZIPCode"] = new OpenApiString("12345")
                },

            });

            return options;
        }
    }
}
using BuildingBlock.Domain.ValueObjects.Contact;
using BuildingBlock.Domain.ValueObjects.Location;
using BuildingBlock.Domain.ValueObjects.Media;
using BuildingBlock.Domain.ValueObjects.Products;
using BuildingBlock.Domain.ValueObjects.Time;
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

            options.MapType<ProductName>(() => new OpenApiSchema
            {
                Type = "string",
                Title = "Produto",
                Description = "Represent a valid product name.",
                Example = new OpenApiString(ProductName.Empty),
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

            options.MapType<PreparationTime>(() => new OpenApiSchema
            {
                Type = "numeric",
                Title = "Tempo de preparação",
                Description = "Represent a valid tempo de preparação.",
                Example = new OpenApiString(PreparationTime.Empty),
            });

            options.MapType<Image>(() => new OpenApiSchema
            {
                Type = "object",
                Title = "Image",
                Description = "Representa uma imagem.",
                Properties = new Dictionary<string, OpenApiSchema>
                {
                    { "Uri", new OpenApiSchema { Type = "string", Format = "uri" } },
                    { "AlternativeText", new OpenApiSchema { Type = "string" } },
                    { "Width", new OpenApiSchema { Type = "integer", Format = "int32" } },
                    { "Height", new OpenApiSchema { Type = "integer", Format = "int32" } },
                    { "ThumbnailUri", new OpenApiSchema { Type = "string", Format = "uri" } },
                    { "BlurHash", new OpenApiSchema { Type = "string" } }
                },
                Example = new OpenApiObject
                {
                    ["Uri"] = new OpenApiString("https://example.com/images/photo.jpg"),
                    ["AlternativeText"] = new OpenApiString("Uma bela paisagem ao entardecer"),
                    ["Width"] = new OpenApiInteger(1920),
                    ["Height"] = new OpenApiInteger(1080),
                    ["ThumbnailUri"] = new OpenApiString("https://example.com/images/photo-thumb.jpg"),
                    ["BlurHash"] = new OpenApiString("LKO2?U%2Tw=w]~RBVZRi};RPxuwH")
                }
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
                    ["ZIPCode"] = new OpenApiString("12345-000")
                },

            });

            return options;
        }
    }
}
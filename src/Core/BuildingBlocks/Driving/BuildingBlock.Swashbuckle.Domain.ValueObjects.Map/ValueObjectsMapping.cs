using System.Text.Json.Nodes;
using BuildingBlock.Domain.ValueObjects.Business;
using BuildingBlock.Domain.ValueObjects.Contact;
using BuildingBlock.Domain.ValueObjects.Location;
using BuildingBlock.Domain.ValueObjects.Media;
using BuildingBlock.Domain.ValueObjects.Products;
using BuildingBlock.Domain.ValueObjects.Time;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BuildingBlock.Swashbuckle.Domain.ValueObjects.Map
{
    public static class ValueObjectsMapping
    {
        /// <summary>
        /// Configures Swagger schema mappings for domain value objects to enhance API documentation.
        /// </summary>
        /// <returns>The <see cref="SwaggerGenOptions"/> instance with added domain value object mappings.</returns>
        public static SwaggerGenOptions AddDomainMapping(this SwaggerGenOptions options)
        {
            options.MapType<LegalName>(() => new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Title = "LegalName",
                Description = "Represent a valid Name.",
                Example = LegalName.Empty,
            });

            options.MapType<TradeName>(() => new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Title = "TradeName",
                Description = "Represent a valid Trade Name.",
                Example = TradeName.Empty,
            });

            options.MapType<CpfCnpj>(() => new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Title = "CpfCnpj",
                Description = "Represent a valid Cpf/Cnpj.",
                Example = CpfCnpj.Empty,
            });

            options.MapType<StateRegistration>(() => new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Title = "StateRegistration",
                Description = "Represent a valid State Registration.",
                Example = StateRegistration.Empty,
            });

            options.MapType<MunicipalRegistration>(() => new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Title = "MunicipalRegistration",
                Description = "Represent a valid Municipal Registration.",
                Example = MunicipalRegistration.Empty,
            });

            options.MapType<Email>(() => new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Title = "Email",
                Description = "Represent a valid email.",
                Example = Email.Empty,
            });

            options.MapType<ProductName>(() => new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Title = "Produto",
                Description = "Represent a valid product name.",
                Example = ProductName.Empty,
            });

            options.MapType<PersonName>(() => new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Title = "Name",
                Description = "Represent a valid person name.",
                Example = PersonName.Empty,
            });

            options.MapType<Phone>(() => new OpenApiSchema
            {
                Type = JsonSchemaType.String,
                Title = "Phone",
                Description = "Represent a valid phone number.",
                Example = Phone.Empty,
            });

            options.MapType<PreparationTime>(() => new OpenApiSchema
            {
                Type = JsonSchemaType.Number,
                Title = "Tempo de preparação",
                Description = "Represent a valid tempo de preparação.",
                Example = PreparationTime.Empty,
            });

            options.MapType<Image>(() => new OpenApiSchema
            {
                Type = JsonSchemaType.Object,
                Title = "Image",
                Description = "Representa uma imagem.",
                Properties = new Dictionary<string, IOpenApiSchema>
                {
                    { "Uri", new OpenApiSchema { Type = JsonSchemaType.String, Format = "uri" } },
                    { "AlternativeText", new OpenApiSchema { Type = JsonSchemaType.String } },
                    { "Width", new OpenApiSchema { Type = JsonSchemaType.Integer, Format = "int32" } },
                    { "Height", new OpenApiSchema { Type = JsonSchemaType.Integer, Format = "int32" } },
                    { "ThumbnailUri", new OpenApiSchema { Type = JsonSchemaType.String, Format = "uri" } },
                    { "BlurHash", new OpenApiSchema { Type = JsonSchemaType.String } }
                },
                Example = new JsonObject
                {
                    ["Uri"] = "https://example.com/images/photo.jpg",
                    ["AlternativeText"] = "Uma bela paisagem ao entardecer",
                    ["Width"] = 1920,
                    ["Height"] = 1080,
                    ["ThumbnailUri"] = "https://example.com/images/photo-thumb.jpg",
                    ["BlurHash"] = "LKO2?U%2Tw=w]~RBVZRi};RPxuwH"
                }
            });

            options.MapType<Address>(() => new OpenApiSchema
            {
                Type = JsonSchemaType.Object,
                Title = "Address",
                Description = "Represent a valid address.",
                Properties = new Dictionary<string, IOpenApiSchema>
                {
                    { "Street", new OpenApiSchema { Type = JsonSchemaType.String } },
                    { "Number", new OpenApiSchema { Type = JsonSchemaType.String } },
                    { "Complement", new OpenApiSchema { Type = JsonSchemaType.String } },
                    { "City", new OpenApiSchema { Type = JsonSchemaType.String } },
                    { "State", new OpenApiSchema { Type = JsonSchemaType.String } },
                    { "ZIPCode", new OpenApiSchema { Type = JsonSchemaType.String } }
                },
                Example = new JsonObject()
                {
                    ["Street"] = "123 Main St",
                    ["Number"] = "Apt 4B",
                    ["Complement"] = "Building XYZ",
                    ["City"] = "Example Ville",
                    ["State"] = "EX",
                    ["ZIPCode"] = "12345-000"
                },
            });

            return options;
        }
    }
}
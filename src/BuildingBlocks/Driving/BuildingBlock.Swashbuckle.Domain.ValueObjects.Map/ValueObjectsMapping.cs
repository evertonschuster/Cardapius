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
                Example = new OpenApiString(Email.Empty),
            });

            options.MapType<PersonName>(() => new OpenApiSchema
            {
                Type = "string",
                Example = new OpenApiString(PersonName.Empty),
            });

            options.MapType<Phone>(() => new OpenApiSchema
            {
                Type = "string",
                Example = new OpenApiString(Phone.Empty),
            });

            return options;
        }
    }
}
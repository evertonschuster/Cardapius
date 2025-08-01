﻿using BuildingBlock.Api.Domain.ValueObjects.Json.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlock.Api.Domain.ValueObjects.Json.Extensions
{
    public static class DomainDataExtension
    {
        public static void AddApplicationDomainDataJsonConvert(this IServiceCollection services)
        {
            services.ConfigureOptions<JsonOptionsConfiguration>();
        }
    }
}

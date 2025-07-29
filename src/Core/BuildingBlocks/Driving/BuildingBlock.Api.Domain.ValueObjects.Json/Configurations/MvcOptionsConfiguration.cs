using BuildingBlock.Api.Domain.ValueObjects.Json.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BuildingBlock.Api.Domain.ValueObjects.Json.Configurations
{
    internal class MvcOptionsConfiguration : IConfigureOptions<MvcOptions>
    {
        /// <summary>
        /// Adds a custom model validator provider to the specified <see cref="MvcOptions"/> instance.
        /// </summary>
        /// <param name="options">The MVC options to configure.</param>
        public void Configure(MvcOptions options)
        {
            options.ModelValidatorProviders.Add(new ValidatableModelValidatorProvider());
        }
    }
}

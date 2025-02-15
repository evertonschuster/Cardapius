using BuildingBlock.Api.Domain.ValueObjects.Json.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BuildingBlock.Api.Domain.ValueObjects.Json.Configurations
{
    internal class MvcOptionsFormattersConfigure : IConfigureOptions<MvcOptions>
    {
        public void Configure(MvcOptions options)
        {
            options.ModelValidatorProviders.Add(new ValueObjectModelValidatorProvider());
        }
    }
}

using BuildingBlock.Domain.ValueObject.Json.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BuildingBlock.Domain.ValueObject.Json.Configurations
{
    internal class MvcOptionsFormattersConfigure : IConfigureOptions<MvcOptions>
    {
        private readonly ILogger<DomainDataJsonInputFormatter> _logger;
        private readonly JsonOptions _jsonOpts;
        public MvcOptionsFormattersConfigure(IOptions<JsonOptions> options, ILogger<DomainDataJsonInputFormatter> logger)
        {
            _logger = logger;
            _jsonOpts = options.Value;
        }

        public void Configure(MvcOptions options)
        {
            var formatter = new DomainDataJsonInputFormatter(_jsonOpts, _logger);
            options.InputFormatters.Insert(0, formatter);
        }
    }
}

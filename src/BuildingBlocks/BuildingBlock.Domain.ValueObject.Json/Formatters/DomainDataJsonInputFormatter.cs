using BuildingBlock.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace BuildingBlock.Domain.ValueObject.Json.Formatters
{
    public class DomainDataJsonInputFormatter : SystemTextJsonInputFormatter
    {
        public DomainDataJsonInputFormatter(JsonOptions options, ILogger<DomainDataJsonInputFormatter> logger) : base(options, logger)
        {
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            try
            {
                return await base.ReadRequestBodyAsync(context);
            }
            catch (BusinessException businessException)
            {
                var key = businessException.KeyError ?? string.Empty;
                key = char.ToLower(key[0]) + key[1..];

                var inputFormatterException = new InputFormatterException(businessException.Message, businessException);
                context.ModelState.TryAddModelError(key, inputFormatterException, context.Metadata);

                return InputFormatterResult.Failure();
            }
        }
    }
}

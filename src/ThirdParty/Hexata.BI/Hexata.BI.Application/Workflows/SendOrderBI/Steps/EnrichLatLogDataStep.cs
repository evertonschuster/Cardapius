using Hexata.BI.Application.Services.Localizations;
using Hexata.BI.Application.Workflows.SendOrderBI.Models;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hexata.BI.Application.Workflows.SendOrderBI.Steps
{
    public class EnrichLatLogDataStep(ILocalizationService localizationService) : StepBodyAsync
    {
        public Order Order { get; internal set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            if (Order.Customer == null)
            {
                return ExecutionResult.Next();
            }

            var adress = new AddressDto
            {
                Street = Order.Customer.Address,
                Number = Order.Customer.AddressNumber,
                Neighborhood = Order.Customer.Neighborhood,
                City = Order.Customer.City,
                State = Order.Customer.State,
                PostalCode = Order.Customer.ZipCode
            };

            var localization = await localizationService.GetLocalizationAsync(adress);

            if (localization.IsSuccess)
            {
                Order.Localization = localization.Value?.Localization;
            }

            return ExecutionResult.Next();
        }
    }
}


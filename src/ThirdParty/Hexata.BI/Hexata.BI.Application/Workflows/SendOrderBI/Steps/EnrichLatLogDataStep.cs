using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Services.Localization;
using Hexata.BI.Application.Workflows.SendOrderBI.Dtos;
using Hexata.BI.Application.Workflows.SendOrderBI.Dtos.Google;
using Hexata.BI.Application.Workflows.SendOrderBI.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                Order.Localization = localization.Value;
            }

            return ExecutionResult.Next();
        }
    }
}


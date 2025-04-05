using Hexata.BI.Application.Repositories;
using Hexata.BI.Application.Workflows.SendOrderBI.Models;
using Microsoft.Extensions.Logging;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hexata.BI.Application.Workflows.SendOrderBI.Steps
{
    public class SaveDataStep(ILogger<SaveDataStep> logger, IRepository<Order, int> repository) : StepBodyAsync
    {
        public List<Order> Orders { get; internal set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            logger.LogInformation("Save data in Mongo");
            await repository.UpsertMultipleAsync(Orders);

            return ExecutionResult.Next();
        }
    }
}

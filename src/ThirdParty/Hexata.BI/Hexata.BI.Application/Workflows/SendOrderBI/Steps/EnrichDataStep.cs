using System.Data;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hexata.BI.Application.Workflows.SendOrderBI.Steps
{
    class EnrichDataStep : StepBodyAsync
    {
        private readonly IDbConnection dbConnection;
        private readonly SendOrderBIInstrument sendOrderBIInstrument;

        public override Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            throw new NotImplementedException();
        }
    }
}

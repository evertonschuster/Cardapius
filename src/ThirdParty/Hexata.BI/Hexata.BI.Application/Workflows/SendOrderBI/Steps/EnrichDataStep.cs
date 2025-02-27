using Hexata.BI.Application.Observabilities;
using Microsoft.Extensions.Logging;
using System.Data;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hexata.BI.Application.Workflows.SendOrderBI.Steps
{
    class EnrichDataStep(IDbConnection dbConnection, Instrument sendOrderBIInstrument, ILogger<EnrichDataStep> logger, Instrument instrument) : StepBodyAsync
    {
        private readonly IDbConnection dbConnection = dbConnection;
        private readonly Instrument sendOrderBIInstrument = sendOrderBIInstrument;
        private readonly ILogger<EnrichDataStep> logger = logger;
        private readonly Instrument instrument = instrument;

        public override Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {

            return Task.FromResult(ExecutionResult.Next());
        }
    }
}

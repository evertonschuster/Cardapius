using Dapper;
using Hexata.BI.Application.Observabilities;
using System.Data;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hexata.BI.Application.Workflows.SendOrderBI.Steps
{
    class InitializeExtractDataStep(IDbConnection dbConnection, Instrument instrument) : StepBodyAsync
    {
        private readonly IDbConnection dbConnection = dbConnection;
        private readonly Instrument instrument = instrument;

        public int Total { get; internal set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            using var activity = instrument.ExecuteDataBaseQuery();

            Total = await dbConnection.QuerySingleOrDefaultAsync<int>(@"SELECT COUNT(1) as Total FROM SAIDAS;");

            return ExecutionResult.Outcome(new SendOrderBIData
            {
                Total = 15000
            });
        }
    }
}

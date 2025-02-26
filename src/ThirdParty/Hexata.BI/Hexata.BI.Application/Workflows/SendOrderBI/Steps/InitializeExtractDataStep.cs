using Dapper;
using System.Data;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Hexata.BI.Application.Workflows.SendOrderBI.Steps
{
    class InitializeExtractDataStep : StepBodyAsync
    {
        private readonly IDbConnection dbConnection;
        private readonly SendOrderBIInstrument sendOrderBIInstrument;

        public InitializeExtractDataStep(IDbConnection dbConnection, SendOrderBIInstrument sendOrderBIInstrument)
        {
            this.dbConnection = dbConnection;
            this.sendOrderBIInstrument = sendOrderBIInstrument;
        }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            using var activity = sendOrderBIInstrument.ExecuteDataBaseQuery();

            var total = await dbConnection.QuerySingleOrDefaultAsync<int>(@"SELECT COUNT(1) as Total FROM SAIDAS;");

            return ExecutionResult.Outcome(new SendOrderBIData
            {
                Total = total
            });
        }
    }
}

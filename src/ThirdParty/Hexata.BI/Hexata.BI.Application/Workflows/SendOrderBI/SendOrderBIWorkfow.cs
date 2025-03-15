using Hexata.BI.Application.Workflows.SendOrderBI.Dtos;
using Hexata.BI.Application.Workflows.SendOrderBI.Steps;
using WorkflowCore.Interface;

namespace Hexata.BI.Application.Workflows.SendOrderBI
{
    public class SendOrderBIWorkfow : IWorkflow<SendOrderBIData>
    {
        public static string WorkerId => nameof(SendOrderBIWorkfow);
        public string Id => WorkerId;
        public int Version => 1;

        public void Build(IWorkflowBuilder<SendOrderBIData> builder)
        {
            builder
                .StartWith<InitializeExtractDataStep>()
                .Output(data => data.Total, step => step.Total)
                .While(data => data.Page == 0 || data.Page * data.PageSize < data.Total)
                .Do(then =>
                {
                    then
                        .StartWith<ExtractHexataDataStep>()
                            .Input(step => step.Page, data => data.Page)
                            .Input(step => step.PageSize, data => data.PageSize)
                            .Output(step => step.Page, data => data.NextPage)
                            .Output(step => step.Sales, data => data.Sales)
                        .ForEach(data => data.Sales)
                        .Do(then =>
                        {
                            then.StartWith<ParseDataStep>()
                                .Input(step => step.SaleDto, (data, context) => context.Item as SaleDto)
                                .Output(step => step.Order, data => data.Order)
                            .Then<EnrichLatLogDataStep>()
                                .Input(step => step.Order, (data, context) => data.Order);
                        });
                });
        }
    }
}

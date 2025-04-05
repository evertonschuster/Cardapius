using Hexata.BI.Application.Workflows.SendOrderBI.Steps;
using WorkflowCore.Interface;

namespace Hexata.BI.Application.Workflows.SendOrderBI
{
    public class SendOrderBIJob(IWorkflowHost workflowHost, SaveDataStep saveDataStep)
    {
        private readonly IWorkflowHost workflowHost = workflowHost;

        public async Task ExecutarWorkflowAsync()
        {
            await workflowHost.StartWorkflow(SendOrderBIWorkfow.WorkerId);
        }
    }
}

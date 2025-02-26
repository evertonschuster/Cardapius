using Hexata.BI.Application.Workflows.SendOrderBI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkflowCore.Interface;

namespace Hexata.BI.Application.Extensions
{
    public static class WorkflowCoreExtension
    {
        public static IHostApplicationBuilder AddWorkflowApp(this IHostApplicationBuilder builder)
        {
            builder.Services.AddSingleton<SendOrderBIJob>();
            builder.Services.AddWorkflow();

            return builder;
        }

        public static IHost UseWorkflow(this IHost host)
        {
            var workHost = host.Services.GetService<IWorkflowHost>()!;
            workHost.RegisterWorkflow<SendOrderBIWorkfow, SendOrderBIData>();

            workHost.Start();

            return host;
        }
    }
}

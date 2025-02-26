﻿using Hexata.BI.Application.Observabilities;
using Hexata.BI.Application.Workflows.SendOrderBI;
using Hexata.BI.Application.Workflows.SendOrderBI.Steps;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using WorkflowCore.Interface;

namespace Hexata.BI.Application.Extensions
{
    public static class WorkflowCoreExtension
    {
        public static IHostApplicationBuilder AddWorkflowApp(this IHostApplicationBuilder builder)
        {
            builder.Services.AddSingleton(new ActivitySource("Hexata.BI"));
            builder.Services.AddSingleton<Instrument>();
            builder.Services.AddSingleton<SendOrderBIJob>();

            builder.Services.AddTransient<InitializeExtractDataStep>();
            builder.Services.AddTransient<ExtractHexataDataStep>();

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
